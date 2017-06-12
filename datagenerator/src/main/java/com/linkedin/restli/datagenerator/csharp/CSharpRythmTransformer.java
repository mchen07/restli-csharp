package com.linkedin.restli.datagenerator.csharp;


import com.alibaba.fastjson.JSON;
import com.linkedin.data.schema.DataSchema;
import com.linkedin.data.schema.RecordDataSchema;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.sun.prism.impl.Disposer;
import java.util.HashMap;
import java.util.Map;
import java.util.Optional;
import javafx.util.Pair;
import org.rythmengine.extension.Transformer;


/**
 * @author Evan Williams
 */
@Transformer("restli")
public class CSharpRythmTransformer {
  private static final Map<DataSchema.Type, String> typeNameMap = new HashMap<>();
  static
  {
    typeNameMap.put(DataSchema.Type.INT, "int");
    typeNameMap.put(DataSchema.Type.DOUBLE, "double");
    typeNameMap.put(DataSchema.Type.BOOLEAN, "bool");
    typeNameMap.put(DataSchema.Type.STRING, "string");
    typeNameMap.put(DataSchema.Type.FLOAT, "float");
    typeNameMap.put(DataSchema.Type.LONG, "long");
  }

  public static String comment(String comment) {
    if (comment == null || comment.isEmpty()) {
      return "";
    } else if (comment.contains("\n")) {
      return "/// <summary>\n/// " + comment.replaceAll("\n", "\n/// ") + "\n/// </summary>";
    } else {
      return "/// <summary>" + comment + "</summary>";
    }
  }

  public static String getType(RecordDataSchema.Field field) {
    if (field == null) {
      return "";
    } else if (field.getType().isPrimitive()) {
      return typeNameMap.get(field.getType().getType()) + (field.getOptional() && field.getType().getType() != DataSchema.Type.STRING ? "?" : "");
    } else {
      return "???"; //TODO FIX THIS
    }
  }

  public static String getNullableType(RecordDataSchema.Field field) {
    return typeNameMap.get(field.getType().getType()) + (field.getType().getType() != DataSchema.Type.STRING ? "?" : "");
  }

  public static String hasDefault(RecordDataSchema.Field field) {
    return Boolean.toString(field.getDefault() != null);
  }

  /**
   * Add n spaces to the start of each line, starting from the second line.
   * obj is Object instead of String because rythm considers the variable
   * created from assigning an template invocation result as an Object.
   *
   * @param obj source string
   * @param n number of spaces
   * @return indented string
   */
  public static String addIndent(Object obj, int n)
  {
    String s = obj.toString();

    String[] lines = s.split(GeneratorConstants.NEWLINE);
    String spaces = CSharpUtil.spaces(n);
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < lines.length; i++)
    {
      if (i > 0)
      {
        sb.append(spaces);
      }
      sb.append(lines[i]);
      if (i < lines.length - 1)
      {
        sb.append(GeneratorConstants.NEWLINE);
      }
    }
    return sb.toString();
  }

  public static String getMemberListEntryType(Object obj) {
    if (obj instanceof Map.Entry) {
      Map.Entry entry = (Map.Entry) obj;
      return ((CSharpUnion.Member) entry.getValue()).type.getName();
    } else {
      return null;
    }
  }
}