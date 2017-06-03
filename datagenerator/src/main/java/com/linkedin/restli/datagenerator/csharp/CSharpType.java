package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
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
public abstract class CSharpType { //TODO abstract?
  private final ClassTemplateSpec _spec;

  public CSharpType(ClassTemplateSpec spec) {
    _spec = spec;
  }

  public String getName() {
    return getSpec().getClassName();
  }

  public String getReference() {
    return getName();
  }

  public ClassTemplateSpec getSpec() {
    return _spec;
  }
}