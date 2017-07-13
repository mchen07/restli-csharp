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
import com.linkedin.pegasus.generator.spec.EnumTemplateSpec;
import java.util.List;


/**
 * @author Evan Williams
 */
public class CSharpEnum extends CSharpComplexType {
  private final EnumTemplateSpec _enum;

  public CSharpEnum(ClassTemplateSpec spec, CSharpType enclosingType) {
    super(spec, enclosingType);
    _enum = (EnumTemplateSpec) spec;
  }

  @Override
  public String getName(NameModifier modifier) {
    switch (modifier) {
      case DATAMAP_SHALLOW:
        return "object";
      case DATAMAP_PARSE:
        return "string";
      default:
        return super.getName(NameModifier.NONE);
    }
  }

  public List<String> getSymbols() {
    return _enum.getSchema().getSymbols();
  }

  public String getSymbolDoc(String symbol) {
    return _enum.getSchema().getSymbolDocs().get(symbol);
  }
}
