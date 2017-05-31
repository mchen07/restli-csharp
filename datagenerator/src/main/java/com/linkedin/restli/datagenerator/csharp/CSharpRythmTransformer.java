package com.linkedin.restli.datagenerator.csharp;


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
      return "///<summary>\n///" + comment.replaceAll("\n", "\n///") + "\n///</summary>";
    } else {
      return "///<summary>" + comment + "</summary>";
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
}