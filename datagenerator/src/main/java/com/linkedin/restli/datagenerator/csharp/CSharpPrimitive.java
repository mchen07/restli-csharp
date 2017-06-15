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
        return "string";
      case BYTES:
        return "Bytes";
      case NULL:
        throw new RuntimeException("NULL DataSchema type is not supported in C#");
      default:
        throw new RuntimeException("Unrecognized primitive DataSchema type");
    }
  }

  private String getNullableOperator(boolean nullable) {
    return nullable ? "?" : "";
  }

  @Override
  public String getInitializationExpression(String identifier) {
    switch (getSpec().getSchema().getType()) {
      case BYTES:
        return "new Bytes(BytesUtil.StringToBytes((string)" + identifier + "))";
      default:
        return "(" + getName(NameModifier.NONE) + ")" + identifier;
    }
  }

  @Override
  public boolean needsCastFromBuilder() {
    switch (getSpec().getSchema().getType()) {
      case STRING:
      case BYTES:
        return false;
      default:
        return true;
    }
  }
}