package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.pegasus.generator.spec.ArrayTemplateSpec;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;


/**
 * @author Evan Williams
 */
public class CSharpArray extends CSharpCollectionType {
  private final ArrayTemplateSpec _array;

  public CSharpArray(ClassTemplateSpec spec, CSharpDataTemplateGenerator dataTemplateGenerator) {
    super(spec);
    _array = (ArrayTemplateSpec) spec;
    ClassTemplateSpec itemSpec = _array.getCustomInfo() != null ? CSharpUtil.templateSpecFromCustomInfo(
        _array.getCustomInfo()) : _array.getItemClass();
    _elementType = dataTemplateGenerator.generate(itemSpec, _array.getItemDataClass());
  }

  @Override
  public String getName() {
    return "IReadOnlyList<" + getElementType().getName() + ">";
  }
}
