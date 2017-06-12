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
  public String getName() {
    return getName(false);
  }

  public String getName(boolean optional) {
    switch (getSpec().getSchema().getType()) {
      case BOOLEAN:
        return "bool" + getOptionalOperator(optional);
      case LONG:
        return "long" + getOptionalOperator(optional);
      case INT:
        return "int" + getOptionalOperator(optional);
      case FLOAT:
        return "float" + getOptionalOperator(optional);
      case DOUBLE:
        return "double" + getOptionalOperator(optional);
      case STRING:
      case BYTES:
        return "string";
      case NULL:
        throw new RuntimeException("NULL DataSchema type is not supported in C#");
      default:
        throw new RuntimeException("Unrecognized primitive DataSchema type");
    }
  }

  private String getOptionalOperator(boolean optional) {
    return optional ? "?" : "";
  }
}