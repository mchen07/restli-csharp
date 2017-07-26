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
  public String getName(NameModifier modifier) {
    switch (modifier) {
      case MUTABLE:
        return "List<" + getElementType().getName(NameModifier.MUTABLE) + ">";
      case BUILDER_NULLABLE:
      case BUILDER:
        return "List<" + getElementType().getName(NameModifier.BUILDER) + ">";
      case MUTABLE_SHALLOW:
        return "List<" + getElementType().getName(NameModifier.IMMUTABLE) + ">";
      case TYPED_DATAMAP:
        return "List<" + getElementType().getName(NameModifier.TYPED_DATAMAP) + ">";
      case GENERIC_DATAMAP:
        return "List<object>";
      default:
        return "IReadOnlyList<" + getElementType().getName(NameModifier.IMMUTABLE) + ">";
    }
  }

  @Override
  public String coerceToDataMapExpression(SequentialIdentifierGenerator generator) {
    String identifier = generator.generateIdentifier();
    return String.format(".Select(%1$s => %1$s%2$s).Cast<object>().ToList()", identifier, getElementType().coerceToDataMapExpression(generator));
  }

  @Override
  public String coerceFromDataMapExpression(SequentialIdentifierGenerator generator, String previousIdentifier) {
    String identifier = generator.generateIdentifier();
    return String.format("(%4$s)(%1$s.Select(%2$s => %3$s).ToList())",
        previousIdentifier,
        identifier,
        getElementType().coerceFromDataMapExpression(generator, identifier),
        getName(NameModifier.IMMUTABLE));
  }
}
