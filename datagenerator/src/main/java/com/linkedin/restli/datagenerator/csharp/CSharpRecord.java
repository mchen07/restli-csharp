package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.data.ByteString;
import com.linkedin.data.schema.DataSchema;
import com.linkedin.pegasus.generator.CodeUtil;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.RecordTemplateSpec;
import java.util.AbstractMap;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.TreeSet;
import java.util.regex.Pattern;
import java.util.stream.Collectors;
import org.apache.commons.lang3.StringEscapeUtils;


/**
 * @author Evan Williams
 */
public class CSharpRecord extends CSharpComplexType {
  public enum FieldOptionalFilter {
    REQUIRED,
    OPTIONAL,
    BOTH;

    private boolean isMatch(boolean isOptional) {
      return this == BOTH || (this == OPTIONAL && isOptional) || (this == REQUIRED && !isOptional);
    }
  }

  public enum FieldDefaultValueFilter {
    WITH_VALUE,
    WITHOUT_VALUE,
    BOTH;

    private boolean isMatch(boolean hasDefaultValue) {
      return this == BOTH || (this == WITH_VALUE && hasDefaultValue) || (this == WITHOUT_VALUE && !hasDefaultValue);
    }
  }

  private final CSharpDataTemplateGenerator _dataTemplateGenerator;
  private final RecordTemplateSpec _record;
  private List<Field> _fields;

  public CSharpRecord(ClassTemplateSpec spec, CSharpType enclosingType, CSharpDataTemplateGenerator dataTemplateGenerator) {
    super(spec, enclosingType);
    _dataTemplateGenerator = dataTemplateGenerator;
    _record = (RecordTemplateSpec) spec;
  }

  public boolean containsFieldWithName(String fieldName, List<Pattern> skipDeprecatedArgs)  {
    for (Field field : getFields(skipDeprecatedArgs)) {
      if (field.getName().equals(fieldName))  {
        return true;
      }
    }

    return false;
  }

  public List<Field> getFields() {
    return getFields(new ArrayList<Pattern>());
  }

//  @Override
//  public Set<CSharpComplexType> getReferencedComplexTypes(List<Pattern> skipDeprecatedArgs) {
//    final Set<CSharpComplexType> referenced = new TreeSet<>(nameComparator);
//
//    for (Field field : getFields(skipDeprecatedArgs)) {
//      final CSharpType fieldType = field.getType();
//      if (fieldType instanceof CSharpComplexType) {
//        referenced.add((CSharpComplexType) fieldType);
//      } else if (fieldType instanceof CSharpCollectionType) {
//        final CSharpCollectionType collection = (CSharpCollectionType) fieldType;
//        if (collection.getTerminalType() instanceof CSharpComplexType) {
//          referenced.add((CSharpComplexType) collection.getTerminalType());
//        }
//      }
//    }
//
//    referenced.remove(this);
//
//    return referenced;
//  }
//
  public List<Field> getFields(List<Pattern> skipDeprecatedArgs) {
    return getFields(FieldOptionalFilter.BOTH, FieldDefaultValueFilter.BOTH, skipDeprecatedArgs);
  }

  public List<Field> getFields(FieldOptionalFilter optionalFilter, FieldDefaultValueFilter defaultValueFilter, List<Pattern> skipDeprecatedArgs) {
    if (_fields == null) {
      _fields = _record.getFields().stream()
          .filter(f -> skipDeprecatedArgs.isEmpty() || !(
              _dataTemplateGenerator.shouldCheckDeprecated(getSpec(), skipDeprecatedArgs) && _dataTemplateGenerator.isDeprecated(f)))
          .map(specField -> new AbstractMap.SimpleImmutableEntry<>(_dataTemplateGenerator.generate(
              (specField.getCustomInfo() != null) ? CSharpUtil.templateSpecFromCustomInfo(specField.getCustomInfo()) : specField.getType(), specField.getDataClass()), specField))
          .map(e -> new Field(e.getKey(), e.getValue()))
          .collect(Collectors.toList());
    }

    if (optionalFilter == FieldOptionalFilter.BOTH && defaultValueFilter == FieldDefaultValueFilter.BOTH) {
      return _fields;
    } else {
      return _fields.stream()
          .filter(f -> optionalFilter.isMatch(f.isOptional()) && defaultValueFilter.isMatch(f.hasDefaultValue()))
          .collect(Collectors.toList());
    }
  }

  public static class Field {
    private final CSharpType _type;
    private final RecordTemplateSpec.Field _specField;

    public Field(CSharpType type, RecordTemplateSpec.Field specField) {
      _type = type;
      _specField = specField;

      if (getName().equals(getType().getName())) {
        throw new IllegalArgumentException("Field name " + getName() + " is the same as its type name, illegal in Objective-C.");
      }
    }

    public CSharpType getType() {
      return _type;
    }

    public String getTypeString(boolean coerceNullable) {
      if (_type instanceof CSharpPrimitive) {
        return ((CSharpPrimitive) _type).getName(coerceNullable || isOptional());
      } else {
        return _type.getName();
      }
    }

    public String getTypeString() {
      return getTypeString(false);
    }

    public RecordTemplateSpec.Field getSpecField() {
      return _specField;
    }

    public String getSchemaFieldName() {
      return _specField.getSchemaField().getName();
    }

    public String getName() {
      String fieldName = getSchemaFieldName();
      return CSharpUtil.escapeReserved(fieldName);
    }

    public String getVariableName() {
      String name = getName();
      return name.substring(0, 1).toUpperCase() + name.substring(1); //TODO CHECk FOR DEFAULT VALUES?
    }

    public String getDoc() {
      return _specField.getSchemaField().getDoc();
    }

    public boolean isOptional() {
      return _specField.getSchemaField().getOptional();
    }

    public boolean hasDefaultValue() {
      return _specField.getSchemaField().getDefault() != null;
    }

    public String getOptionalityComment() {
      return (isOptional() ? "optional" : "required") + (hasDefaultValue() ? ", has default value" : "");
    }

    public String getDefaultValueLiteral() {
      final Object defaultValue = _specField.getSchemaField().getDefault();
      if (defaultValue == null) {
        return null;
      }

      final DataSchema fieldSchema = _specField.getSchemaField().getType();
      if (fieldSchema.isComplex() && fieldSchema.getDereferencedType() != DataSchema.Type.ENUM) {
        final String stringified = CSharpUtil.stringify(getType(), defaultValue);
        if (stringified == null) {
          throw new RuntimeException("Default value for complex types on field " + _specField.getSchemaField().getName() + " is currently not supported by Pegasus C# Data Templates.");
        } else {
          return stringified;
        }
      } else if (defaultValue instanceof String) {
        return "\"" + StringEscapeUtils.escapeJava(defaultValue.toString()) + "\"";
      } else if (defaultValue instanceof ByteString) {
        return "\"" + ((ByteString) defaultValue).asAvroString() + "\"";
      } else if (defaultValue instanceof Float) {
        return defaultValue.toString() + "F";
      } {
        return defaultValue.toString();
      }
    }
  }
}