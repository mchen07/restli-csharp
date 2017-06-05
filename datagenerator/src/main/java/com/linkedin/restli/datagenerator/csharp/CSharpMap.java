package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.MapTemplateSpec;


/**
 * @author Evan Williams
 */
public class CSharpMap extends CSharpCollectionType {
  private final MapTemplateSpec _map;

  public CSharpMap(ClassTemplateSpec spec, CSharpDataTemplateGenerator dataTemplateGenerator) {
    super(spec);
    _map = (MapTemplateSpec) spec;
    ClassTemplateSpec valueSpec = _map.getCustomInfo() != null ? CSharpUtil.templateSpecFromCustomInfo(_map.getCustomInfo()) : _map.getValueClass();
    _elementType = dataTemplateGenerator.generate(valueSpec, _map.getValueDataClass());
  }

  @Override
  public String getName() {
    return "IReadOnlyDictionary<string, " + getElementType().getName() + ">";
  }
}
