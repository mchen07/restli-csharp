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

  /**
   * Returns a mutable list of immutable types. The reason for this is that
   * C# cannot handle casting a nested list/dictionary object; it can only handle
   * casting the outer layer. Thus, the inner types must be immutable.
   * @return Mutable list of immutable element types
   */
  @Override
  public String getNameMutable() { return "List<" + getElementType().getName() + ">"; }

  @Override
  public String getDataMapParseName() {
    return "List<" + getElementType().getDataMapParseName() + ">";
  }
}
