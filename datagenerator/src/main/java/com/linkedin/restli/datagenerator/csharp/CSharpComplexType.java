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

import com.linkedin.data.schema.NamedDataSchema;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import java.io.File;
import java.util.Comparator;


/**
 * @author Evan Williams
 */
public class CSharpComplexType extends CSharpType {
  public static final Comparator<CSharpComplexType> nameComparator = (o1, o2) -> o1.getName().compareTo(o2.getName());

  protected CSharpType _enclosingType;

  public CSharpComplexType(ClassTemplateSpec spec, CSharpType enclosingType) {
    super(spec);
    _enclosingType = enclosingType;
  }

  @Override
  public String getName(NameModifier modifier) {
    switch (modifier) {
      case DATAMAP_PARSE:
        return "Dictionary<string, object>";
      default:
        return CSharpUtil.escapeReserved(super.getName(NameModifier.NONE));
    }
  }

  public CSharpType getEnclosingType() {
    return _enclosingType;
  }

  public String getDoc() {
    return ((NamedDataSchema) getSpec().getSchema()).getDoc();
  }

  public String getNamespace() { return getSpec().getNamespace(); }

  public File getOutputDirectory(File parentPath) {
    String childPath = getNamespace().replaceAll("\\.", "/");
    return new File(parentPath, childPath);
  }

  @Override
  public String getInitializationExpression(String identifier) {
    return "new " + getName(NameModifier.NONE) + "((" + getName(NameModifier.DATAMAP_PARSE) + ")" + identifier + ")";
  }

  @Override
  public String getDataMapExpression(SequentialIdentifierGenerator generator) {
    return ".Data()";
  }
}