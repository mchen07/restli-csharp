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
import com.sun.org.apache.xerces.internal.impl.dv.dtd.NMTOKENDatatypeValidator;
import java.util.ArrayList;
import java.util.Collection;
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
  public enum NameModifier {NONE, NULLABLE, MUTABLE, DEEP_MUTABLE, DATAMAP_PARSE, IN_BUILDER}

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

  public boolean needsCastFromBuilder() { return false; }
}