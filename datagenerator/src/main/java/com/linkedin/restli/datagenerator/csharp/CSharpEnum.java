package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.EnumTemplateSpec;

import java.util.List;


/**
 * @author Evan Williams
 */
public class CSharpEnum extends CSharpComplexType {
  private final EnumTemplateSpec _enum;

  public CSharpEnum(ClassTemplateSpec spec, CSharpType enclosingType) {
    super(spec, enclosingType);
    _enum = (EnumTemplateSpec) spec;
  }

  @Override
  public String getDataMapParseName() {
    return "string";
  }

  public List<String> getSymbols() {
    return _enum.getSchema().getSymbols();
  }

  public String getSymbolDoc(String symbol) {
    return _enum.getSchema().getSymbolDocs().get(symbol);
  }
}
