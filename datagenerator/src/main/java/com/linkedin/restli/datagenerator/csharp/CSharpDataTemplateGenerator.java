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


import com.linkedin.pegasus.generator.spec.ArrayTemplateSpec;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.EnumTemplateSpec;
import com.linkedin.pegasus.generator.spec.MapTemplateSpec;
import com.linkedin.pegasus.generator.spec.PrimitiveTemplateSpec;
import com.linkedin.pegasus.generator.spec.RecordTemplateSpec;
import com.linkedin.pegasus.generator.spec.UnionTemplateSpec;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.regex.Pattern;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


/**
 * Generate C# language binding type from {@link ClassTemplateSpec}.
 *
 * @author Evan Williams
 */
public class CSharpDataTemplateGenerator {
  private static final Logger LOG = LoggerFactory.getLogger(CSharpDataTemplateGenerator.class);
  protected final Map<ClassTemplateSpec, CSharpType> _generatedClasses = new HashMap<>();
  protected final Map<String, ClassTemplateSpec> _generatedClassNames = new HashMap<>();
  protected final StringBuilder _messageBuilder = new StringBuilder();
  protected Set<CSharpType> _unprocessedTypes = new HashSet<>();

  private static final String DEPRECATED = "deprecated";

  public CSharpType generate(ClassTemplateSpec spec) {
    return generate(spec, null);
  }

  public CSharpType generate(ClassTemplateSpec spec, ClassTemplateSpec dataClass) {
    CSharpType result;

    if (spec == null) {
      result = null;
    } else if (isDeprecated(spec)) {
      result = null;
    } else {
      result = _generatedClasses.get(spec);

      if (result == null) {
        try {
          final ClassTemplateSpec enclosingClass = spec.getEnclosingClass();
          final CSharpType enclosingType;

          if (enclosingClass == null) {
            enclosingType = null;
          } else {
            enclosingType = generate(enclosingClass, dataClass);
          }

          if (spec instanceof ArrayTemplateSpec) {
            result = new CSharpArray(spec, this);
          } else if (spec instanceof EnumTemplateSpec) {
            result = new CSharpEnum(spec, enclosingType);
          } else if (spec instanceof MapTemplateSpec) {
            result = new CSharpMap(spec, this);
          } else if (spec instanceof PrimitiveTemplateSpec) {
            result = new CSharpPrimitive(spec); //ObjCPrimitive(spec);
          } else if (spec instanceof RecordTemplateSpec) {
            result = new CSharpRecord(spec, enclosingType, this);
          } else if (spec instanceof UnionTemplateSpec) {
            result = new CSharpUnion(spec, enclosingType, this);
          } else if (dataClass != null) {
            result = generate(dataClass);
          } else if (enclosingType != null) {
            result = new CSharpComplexType(spec, enclosingType);
          } else {
            throw new IllegalArgumentException("Unrecognized or unsupported class: " + spec.getFullName());
          }

          if (result instanceof CSharpComplexType && !(result instanceof CSharpUnion)) {
            final ClassTemplateSpec originalSpec = _generatedClassNames.get(result.getReference());
            if (originalSpec == null) {
              _generatedClassNames.put(result.getReference(), spec);
            } else {
              LOG.warn(String.format("Duplicate classes detected: %s and %s", originalSpec.getFullName(), spec.getFullName()));
              throw new IllegalArgumentException(
                  String.format("Duplicate classes detected: %s and %s", originalSpec.getFullName(), spec.getFullName()));
            }
          } else if (result instanceof CSharpUnion && enclosingType instanceof CSharpRecord) {
            ((CSharpRecord) enclosingType).addUnnamedUnion((CSharpUnion) result);
          }


          _generatedClasses.put(spec, result);
          _unprocessedTypes.add(result);
        } catch (RuntimeException e) {
          System.out.println("ERROR: " + e.getMessage());
          _messageBuilder.append(e.getMessage()).append("\n");
        }
      }
    }

    return result;
  }

  // Note that this _unprocessedTypes will be reset after this getter. This is used to handle the case where record field
  // type is in resolverPath, but not in input "source". In this case, when we are generating files for the record,
  // the referenced field type will be put into _unprocessedTypes, which will be processed to generate file for
  // referenced field type in the next loop. That is the difference between _unprocessedTypes and _generatedClasses.
  public Set<CSharpType> getUnprocessedTypes() {
    final Set<CSharpType> swap = _unprocessedTypes;
    _unprocessedTypes = new HashSet<>();
    return swap;
  }

  /**
   * Returns whether this spec should be checked for deprecation property
   * @param spec The spec to check deprecation
   * @return whether this spec should be checked for deprecation property
   */
  public static boolean shouldCheckDeprecated(ClassTemplateSpec spec, List<Pattern> skipDeprecatedArgs)
  {
    return skipDeprecatedArgs.stream().anyMatch(pattern -> pattern.matcher(spec.getFullName()).matches());
  }

  /**
   * Returns whether this schema is deprecated
   * @param spec The spec to check the deprecated property
   * @return whether this schema is deprecated
   */
  public boolean isDeprecated(ClassTemplateSpec spec) {
    return hasDeprecatedProperty(spec.getSchema().getProperties(), spec.getFullName());
  }

  /**
   * Returns whether this field is deprecated
   * @param field the field to check the deprecated property
   * @return whether this field is deprecated
   * @throws IllegalArgumentException if a non-optional record field is marked with "deprecated" and skip-deprecated
   * is turned on for this record.
   */
  public boolean isDeprecated(RecordTemplateSpec.Field field) {
    if (hasDeprecatedProperty(field.getSchemaField().getProperties(), field.getSchemaField().getRecord().getFullName()))
    {
      if (!field.getSchemaField().getOptional()) {
        throw new IllegalArgumentException("Fields must be optional to be deprecated: '" + field.getSchemaField().getRecord().getFullName() + "'");
      }
      else
      {
        return true;
      }
    }
    return false;
  }

  /**
   * Returns whether this properties map contains the deprecated property and that it is set to true
   * @param properties the property map to check
   * @param containingSchemaName the schema where this properties is defined in
   * @return whether this properties map contains the deprecated property and that it is set to true
   * @throws IllegalArgumentException if the deprecated property exists but is not a boolean value
   */
  private boolean hasDeprecatedProperty(Map<String, Object> properties, String containingSchemaName) {
    if (properties.containsKey(DEPRECATED)) {
      Object property = properties.get(DEPRECATED);
      if (property instanceof Boolean)
      {
        return (Boolean) properties.get(DEPRECATED);
      }
      else if (property instanceof String)
      {
        return true;
      }
      else
      {
        throw new IllegalArgumentException("Expected boolean or string value for '" + DEPRECATED + "' property in " + containingSchemaName);
      }
    } else {
      return false;
    }
  }
}