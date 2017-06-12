package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;


/**
 * @author Evan Williams
 */
public class CSharpPrimitive extends CSharpType {

public CSharpPrimitive(ClassTemplateSpec spec) {
  super(spec);
}

  @Override
  public String getName(NameModifier modifier) {
    switch (modifier) {
      case NULLABLE:
      case IN_BUILDER:
        return getName(true);
      default:
        return getName(false);
    }
  }

  private String getName(boolean nullable) {
    switch (getSpec().getSchema().getType()) {
      case BOOLEAN:
        return "bool" + getNullableOperator(nullable);
      case LONG:
        return "long" + getNullableOperator(nullable);
      case INT:
        return "int" + getNullableOperator(nullable);
      case FLOAT:
        return "float" + getNullableOperator(nullable);
      case DOUBLE:
        return "double" + getNullableOperator(nullable);
      case STRING:
      case BYTES:
        return "string";
      case NULL:
        throw new RuntimeException("NULL DataSchema type is not supported in C#");
      default:
        throw new RuntimeException("Unrecognized primitive DataSchema type");
    }
  }

  private String getNullableOperator(boolean nullable) {
    return nullable ? "?" : "";
  }
}