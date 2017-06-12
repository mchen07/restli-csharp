using System.Collections.Generic;
using System;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com/linkedin/restli/datagenerator/integration/PrimitiveOptionalRecord.pdsc

namespace com.linkedin.restli.datagenerator.integration
{
  /// <summary>
  /// Contains fields of all supported primitive schema types
  /// Every field is optional
  /// </summary>
  public class PrimitiveOptionalRecord
  {
    /// <summary>I will make the doc look casual</summary>
    // optional, has default value
    public bool? booleanField { get; }
    public bool hasBooleanField { get; }
    /// <summary>so that you know it is not codegen-ed</summary>
    // optional, has default value
    public int? intField { get; }
    public bool hasIntField { get; }

    // optional
    public long? longField { get; }

    /// <summary>intentionally not leave a doc for the previous field</summary>
    // optional, has default value
    public float? floatField { get; }
    public bool hasFloatField { get; }

    // optional, has default value
    public double? doubleField { get; }
    public bool hasDoubleField { get; }

    // optional
    public string stringField { get; }


    public PrimitiveOptionalRecord(Dictionary<string, object> data)
    {
      object value;
      // Retrieve data for booleanField
      if (data.TryGetValue("booleanField", out value))
      {
        booleanField = (bool)value;
        hasBooleanField = true;
      }
      else
      {
        booleanField = false;
        hasBooleanField = false;
      }
      // Retrieve data for intField
      if (data.TryGetValue("intField", out value))
      {
        intField = (int)value;
        hasIntField = true;
      }
      else
      {
        intField = 123;
        hasIntField = false;
      }
      // Retrieve data for longField
      if (data.TryGetValue("longField", out value))
      {
        longField = (long)value;

      }

      // Retrieve data for floatField
      if (data.TryGetValue("floatField", out value))
      {
        floatField = (float)value;
        hasFloatField = true;
      }
      else
      {
        floatField = 23.4F;
        hasFloatField = false;
      }
      // Retrieve data for doubleField
      if (data.TryGetValue("doubleField", out value))
      {
        doubleField = (double)value;
        hasDoubleField = true;
      }
      else
      {
        doubleField = 3.14159;
        hasDoubleField = false;
      }
      // Retrieve data for stringField
      if (data.TryGetValue("stringField", out value))
      {
        stringField = (string)value;

      }

    }

    public PrimitiveOptionalRecord(PrimitiveOptionalRecordBuilder builder)
    {
      // Retrieve data for booleanField
      if (builder.booleanField != null)
      {
        booleanField = (bool?)builder.booleanField;
        hasBooleanField = true;
      }
      else
      {
        booleanField = false;
        hasBooleanField = false;
      }
      // Retrieve data for intField
      if (builder.intField != null)
      {
        intField = (int?)builder.intField;
        hasIntField = true;
      }
      else
      {
        intField = 123;
        hasIntField = false;
      }
      // Retrieve data for longField
      if (builder.longField != null)
      {
        longField = (long?)builder.longField;

      }

      // Retrieve data for floatField
      if (builder.floatField != null)
      {
        floatField = (float?)builder.floatField;
        hasFloatField = true;
      }
      else
      {
        floatField = 23.4F;
        hasFloatField = false;
      }
      // Retrieve data for doubleField
      if (builder.doubleField != null)
      {
        doubleField = (double?)builder.doubleField;
        hasDoubleField = true;
      }
      else
      {
        doubleField = 3.14159;
        hasDoubleField = false;
      }
      // Retrieve data for stringField
      if (builder.stringField != null)
      {
        stringField = (string)builder.stringField;

      }

    }

  }

  public class PrimitiveOptionalRecordBuilder
  {
    public bool? booleanField { get; set; }
    public int? intField { get; set; }
    public long? longField { get; set; }
    public float? floatField { get; set; }
    public double? doubleField { get; set; }
    public string stringField { get; set; }

    public PrimitiveOptionalRecord Build()
    {
      return new PrimitiveOptionalRecord(this);
    }
  }
}