package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.data.schema.NamedDataSchema;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;

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
  public String getName() {
    return CSharpUtil.escapeReserved(super.getName()); //_enclosingType == null ? "" : _enclosingType.getName()); TODO what?
  }

  public CSharpType getEnclosingType() {
    return _enclosingType;
  }

  public String getDoc() {
    return ((NamedDataSchema) getSpec().getSchema()).getDoc();
  }
}