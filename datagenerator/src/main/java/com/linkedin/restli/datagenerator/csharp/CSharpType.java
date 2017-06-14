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
}