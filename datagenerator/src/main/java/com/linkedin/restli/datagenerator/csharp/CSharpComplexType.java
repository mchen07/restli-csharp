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
}