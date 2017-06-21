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


/**
 * @author Evan Williams
 */
public class CSharpPrimitive extends CSharpType {

public CSharpPrimitive(ClassTemplateSpec spec) {
  super(spec);
}

  @Override
  public String getName(NameModifier modifier) {
    switch (modifier) {
      case NULLABLE:
      case IN_BUILDER:
        return getName(true);
      default:
        return getName(false);
    }
  }

  private String getName(boolean nullable) {
    switch (getSpec().getSchema().getType()) {
      case BOOLEAN:
        return "bool" + getNullableOperator(nullable);
      case LONG:
        return "long" + getNullableOperator(nullable);
      case INT:
        return "int" + getNullableOperator(nullable);
      case FLOAT:
        return "float" + getNullableOperator(nullable);
      case DOUBLE:
        return "double" + getNullableOperator(nullable);
      case STRING:
        return "string";
      case BYTES:
        return "Bytes";
      case NULL:
        throw new RuntimeException("NULL DataSchema type is not supported in C#");
      default:
        throw new RuntimeException("Unrecognized primitive DataSchema type");
    }
  }

  private String getNullableOperator(boolean nullable) {
    return nullable ? "?" : "";
  }

  @Override
  public String getInitializationExpression(String identifier) {
    switch (getSpec().getSchema().getType()) {
      case LONG:
        return "Convert.ToInt64(" + identifier + ")";
      case INT:
        return "Convert.ToInt32(" + identifier + ")";
      case FLOAT:
        return "Convert.ToSingle(" + identifier + ")";
      case DOUBLE:
        return "Convert.ToDouble(" + identifier + ")";
      case BYTES:
        return "new Bytes(BytesUtil.StringToBytes((string)" + identifier + "))";
      default:
        return "(" + getName(NameModifier.NONE) + ")" + identifier;
    }
  }

  @Override
  public boolean needsCastFromBuilder() {
    switch (getSpec().getSchema().getType()) {
      case STRING:
      case BYTES:
        return false;
      default:
        return true;
    }
  }
}