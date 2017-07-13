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
    NONE,           // immutable recursive
    NULLABLE,       // immutable recursive, nullable if outer layer primitive
    MUTABLE,        // outer later mutable, else immutable recursive
    DEEP_MUTABLE,   // mutable recursive
    DATAMAP_PARSE,  // mutable recursive, complex types represented as data maps
    DATAMAP_SHALLOW,// mutable, outer layer only, complex types represented as data maps
    BUILDER_OUTER,  // mutable, recursive calls are BUILDER_INNER, nullable, unions with fully qualified name
    BUILDER_INNER   // mutable recursive, unions with fully qualified name
  }

  public CSharpType(ClassTemplateSpec spec) {
    _spec = spec;
  }

  public final String getName() {
    return getName(NameModifier.NONE);
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