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
import javax.naming.OperationNotSupportedException;


/**
 * @author Evan Williams
 */
public abstract class CSharpCollectionType extends CSharpType {
  protected CSharpType _elementType;

  public CSharpCollectionType(ClassTemplateSpec spec) {
    super(spec);
  }

  public CSharpCollectionType(ClassTemplateSpec spec, CSharpType elementType) {
    super(spec);
    _elementType = elementType;
  }

  public CSharpType getElementType() {
    return _elementType;
  }

  public CSharpType getTerminalType() {
    if (getElementType() instanceof CSharpCollectionType) {
      if (_elementType != null) {
        return ((CSharpCollectionType) _elementType).getTerminalType();
      } else {
        throw new IllegalArgumentException("ElementType is not set for CSharpCollectionType!");
      }
    } else {
      return getElementType();
    }
  }

  /*
   * This method should not be used. CSharp collection types cannot be properly initialized
   * with a single expression.
   */
  @Override
  public String getInitializationExpression(String identifier) {
    return null;
  }

  @Override
  public boolean needsCastFromBuilder() {
    return true;
  }
}
