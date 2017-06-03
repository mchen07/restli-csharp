package com.linkedin.restli.datagenerator.csharp;

import com.linkedin.data.DataList;
import com.linkedin.data.DataMap;
import com.linkedin.data.schema.NamedDataSchema;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.CustomInfoSpec;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.regex.Pattern;


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

  public static String stringify(Object obj) {
    if (obj instanceof DataList && ((DataList) obj).isEmpty()) {
      return "new List<>()"; //TODO FIX
    } else if (obj instanceof DataMap && ((DataMap) obj).isEmpty()) {
      return "new Map<>()"; //TODO FIX
    } else {
      return null;
    }
  }
}