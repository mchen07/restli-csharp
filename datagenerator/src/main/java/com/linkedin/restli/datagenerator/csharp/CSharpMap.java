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

  /**
   * If param modifier is MUTABLE:
   *  Returns a mutable map of immutable types. The reason for this is that
   *  C# cannot handle casting a nested list/dictionary object; it can only handle
   *  casting the outer layer. Thus, the inner types must be immutable.
   * If param modifier is DATAMAP_PARSE:
   *  Returns a mutable map of types that are readable by the constructors
   *  of each datamodel. For instance, all record types would be represented
   *  as Dictionary(string, object)
   * Else:
   *  Return immutable map.
   * @return String representation of this map type in C#
   */
  @Override
  public String getName(NameModifier modifier) {
    switch (modifier) {
      case MUTABLE:
        return "Dictionary<string, " + getElementType().getName(NameModifier.NONE) + ">";
      case DATAMAP_PARSE:
        return "Dictionary<string, " + getElementType().getName(NameModifier.DATAMAP_PARSE) + ">";
      default:
        return "IReadOnlyDictionary<string, " + getElementType().getName(NameModifier.NONE) + ">";
    }
  }
}
