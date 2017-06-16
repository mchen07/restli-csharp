/*
Copyright (c) 2017 LinkedIn Corp.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

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
      case DEEP_MUTABLE:
      case IN_BUILDER:
        return "Dictionary<string, " + getElementType().getName(NameModifier.DEEP_MUTABLE) + ">";
      case MUTABLE:
        return "Dictionary<string, " + getElementType().getName(NameModifier.NONE) + ">";
      case DATAMAP_PARSE:
        return "Dictionary<string, " + getElementType().getName(NameModifier.DATAMAP_PARSE) + ">";
      default:
        return "IReadOnlyDictionary<string, " + getElementType().getName(NameModifier.NONE) + ">";
    }
  }
}
