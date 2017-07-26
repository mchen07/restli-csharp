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
import java.util.Collections;
import java.util.List;
import java.util.Set;
import java.util.regex.Pattern;


/**
 * Base type for all C# language binding.
 *
 * @author Evan Williams
 */
public abstract class CSharpType {
  private final ClassTemplateSpec _spec;

  public enum NameModifier {
    // Immutable recursive.
    // used for class field declaration.
    // e.g. int, IReadOnlyList<IReadOnlyList<int>>, etc.
    IMMUTABLE,

    // immutable recursive for complex type, and nullable for primitive type.
    // used for class field declaration.
    // e.g. int?, IReadOnlyList<IReadOnlyList<int>>, etc.
    IMMUTABLE_NULLABLE,

    // outermost layer mutable, and immutable recursive inner layers.
    // used mainly in processing collection types.
    // e.g. int, List<IReadOnlyList<int>>, etc.
    MUTABLE_SHALLOW,

    // mutable recursive.
    // used mainly in processing collection types.
    // e.g. int, List<List<int>>, etc.
    MUTABLE,

    // mutable strongly typed data map representation for a CSharpType.
    // e.g. Dictionary<string, List<Dictionary<string, object>>> (for Dictionary<string, List<FooRecord>>), etc.
    TYPED_DATAMAP,

    // generic data map representation for a CSharpType.
    // e.g. Dictionary<string, object> (for Dictionary<string, List<FooRecord>>), List<object> (for List<List<int>>), etc.
    GENERIC_DATAMAP,

    // used for builder field declaration, where nullable for outermost primitive type
    // and mutable recursive for inner complex types. Fields containing a union type
    // are prefixed with its enclosing class.
    // e.g. int?, List<List<int>>, UnionRecord.UnionField, etc.
    BUILDER_NULLABLE,

    // same as BUILDER_NULLABLE, except not nullable for primitive type (including nested and outermost).
    // e.g. int, List<List<int>>, UnionRecord.UnionField, etc.
    BUILDER
  }

  public CSharpType(ClassTemplateSpec spec) {
    _spec = spec;
  }

  public final String getName() {
    return getName(NameModifier.IMMUTABLE);
  }

  public String getName(NameModifier modifier) { return getSpec().getClassName(); }

  public String getReference() {
    return getName();
  }

  public ClassTemplateSpec getSpec() {
    return _spec;
  }

  public Set<CSharpComplexType> getReferencedComplexTypes(List<Pattern> skipDeprecatedArgs) {
    return Collections.emptySet();
  }

  public abstract String getInitializationExpression(String identifier);

  /**
   * Creates an inline C# expression that - when placed after the identifier -
   * will create a nested expression converting that Rest.li object to a data map.
   * @param baseName base value of the identifier string (e.g. x -> x0, x1, x2, ...)
   * @return expression in C# to convert to data map
   */
  public final String coerceToDataMapExpression(String baseName) {
    SequentialIdentifierGenerator generator = new SequentialIdentifierGenerator(baseName);
    return coerceToDataMapExpression(generator);
  }

  public abstract String coerceToDataMapExpression(SequentialIdentifierGenerator generator);

  public final String coerceToDataMapExpression() {
    return coerceToDataMapExpression("_");
  }

  /**
   * Creates an inline C# expression that - when placed after the identifier -
   * will create a nested expression converting that data map back to a Rest.li object.
   * @param baseName base value of the identifier string (e.g. x -> x0, x1, x2, ...)
   * @return expression in C# to convert from data map
   */
  public final String coerceFromDataMapExpression(String baseName, String previousIdentifier) {
    SequentialIdentifierGenerator generator = new SequentialIdentifierGenerator(baseName);
    return coerceFromDataMapExpression(generator, previousIdentifier);
  }

  public abstract String coerceFromDataMapExpression(SequentialIdentifierGenerator generator, String previousIdentifier);

  public final String coerceFromDataMapExpression(String previousIdentifier) {
    return coerceFromDataMapExpression("_", previousIdentifier);
  }

  public boolean needsCastFromBuilder() { return false; }


}