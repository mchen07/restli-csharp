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

  /**
   * Returns a mutable map of immutable value types. The reason for this is that
   * C# cannot handle casting a nested list/dictionary object; it can only handle
   * casting the outer layer. Thus, the inner types must be immutable.
   * @return Mutable map of immutable value types
   */
  @Override
  public String getNameMutable() { return "Dictionary<string, " + getElementType().getName() + ">"; }

  @Override
  public String getDataMapParseName() {
    return "Dictionary<string, " + getElementType().getDataMapParseName() + ">";
  }
}
