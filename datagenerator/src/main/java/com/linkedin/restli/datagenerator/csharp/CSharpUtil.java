package com.linkedin.restli.datagenerator.csharp;

import com.linkedin.data.DataList;
import com.linkedin.data.DataMap;
import com.linkedin.data.schema.NamedDataSchema;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.CustomInfoSpec;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;
import org.apache.commons.lang3.StringUtils;
import org.rythmengine.RythmEngine;


/**
 * @author Evan Williams
 */
public class CSharpUtil {
  private static final Set<String> _reserved = Collections.unmodifiableSet(new HashSet<>(
      Arrays.asList("abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
          "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit",
          "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int",
          "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out",
          "override", "params", "private", "protected", "public", "readonly", "return", "sbyte", "sealed", "uint",
          "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try",
          "typeof", "ref", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "volatile", "void", "while",
          "List", "IReadOnlyList", "Dictionary", "IReadOnlyDictionary", "Map", "map", "Array", "array",
          "Symbol", "symbol", "Member", "member")));

  public static boolean isReserved(String name) {
    return _reserved.contains(name);
  }

  public static String escapeReserved(String name) {
    if (_reserved.contains(name)) {
      return name + '_';
    }

    return name;
  }

  public static ClassTemplateSpec templateSpecFromCustomInfo(CustomInfoSpec customInfo) {
    NamedDataSchema schema = customInfo.getCustomSchema();
    ClassTemplateSpec spec = ClassTemplateSpec.createFromDataSchema(schema);
    spec.setNamespace(schema.getNamespace());
    spec.setClassName(schema.getName());
    return spec;
  }

  public static String stringify(CSharpType type, Object obj) {
    if (type instanceof CSharpCollectionType) {
      if (((DataList) obj).isEmpty()) {
        return "new " + type.getName(CSharpType.NameModifier.MUTABLE) + "()";
      } else {
        return null;
      }
    } else if (type instanceof CSharpEnum) {
      String enumType = type.getName(CSharpType.NameModifier.NONE);
      if (obj instanceof String) {
        return "new " + enumType + "(\"" + obj + "\")";
      } else {
        return null;
      }
    } else {
      return null;
    }
  }

  // Generate spaces
  public static String spaces(int n)
  {
    return StringUtils.repeat(" ", n);
  }

  /**
   * Invokes the Rythm template with the given name and pass the arguments. This can be used in place of template
   * invocation in Rythm template to workaround Rythm limitation in argument passing.
   */
  public static String invokeRythmTemplate(String template, RythmEngine engine, Object... args) {
    Object[] argsWithEngine = Arrays.copyOf(args, args.length + 1);
    argsWithEngine[args.length] = engine;
    String rendered =
        engine.renderIfTemplateExists(CSharpRythmGenerator.TEMPLATE_PATH_ROOT + '/' + template + ".rythm", argsWithEngine);
    if (rendered.isEmpty()) {
      throw new RuntimeException("Rythm template '" + template + "' does not exist.");
    }
    return rendered;
  }

  public static String toCapitalized(Object obj) {
    if (obj instanceof String) {
      String s = (String) obj;
      return s.substring(0, 1).toUpperCase() + s.substring(1);
    } else {
      return null;
    }
  }
}