package com.linkedin.restli.datagenerator.csharp;

import com.linkedin.data.DataList;
import com.linkedin.data.DataMap;
import com.linkedin.data.schema.NamedDataSchema;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.CustomInfoSpec;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.regex.Pattern;
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
          "typeof", "ref", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "volatile", "void", "while")));

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
      String elementType = ((CSharpCollectionType) type).getElementType().getName();
      if (type instanceof CSharpArray && ((DataList) obj).isEmpty()) {
        return "new IReadOnlyList<" + elementType + ">()";
      } else if (type instanceof CSharpMap && ((DataMap) obj).isEmpty()) {
        return "new IReadOnlyDictionary<string, " + elementType + ">()";
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
}