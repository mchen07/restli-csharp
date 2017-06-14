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
}
