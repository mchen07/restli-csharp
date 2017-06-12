using System.Collections.Generic;
using System;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp

namespace com.linkedin.restli.datagenerator.integration
{
  
  public class SimpleRecord
  {

    // required
    public string stringField { get; }


    // required, has default value
    public int intValue { get; }
    public bool hasIntValue { get; }

    // required, has default value
    public int anotherIntValue { get; }
    public bool hasAnotherIntValue { get; }

    public SimpleRecord(Dictionary<string, object> data)
    {
      object value;
      bool status;
      // Retrieve data for stringField
      if (data.TryGetValue("stringField", out value))
      {
        stringField = (string)value;

      }

      // Retrieve data for intValue
      if (data.TryGetValue("intValue", out value))
      {
        intValue = (int)value;
        hasIntValue = true;
      }
      else
      {
        intValue = 1;
        hasIntValue = false;
      }
      // Retrieve data for anotherIntValue
      if (data.TryGetValue("anotherIntValue", out value))
      {
        anotherIntValue = (int)value;
        hasAnotherIntValue = true;
      }
      else
      {
        anotherIntValue = 2;
        hasAnotherIntValue = false;
      }
    }

    public SimpleRecord(SimpleRecordBuilder builder)
    {
      // Retrieve data for stringField
      if (builder.stringField != null)
      {
        stringField = (string)builder.stringField;

      }
      else
      {
        throw new System.ArgumentNullException("Required field with no default must be included in builder: stringField");
      }
      // Retrieve data for intValue
      if (builder.intValue != null)
      {
        intValue = (int)builder.intValue;
        hasIntValue = true;
      }
      else
      {
        intValue = 1;
        hasIntValue = false;
      }
      // Retrieve data for anotherIntValue
      if (builder.anotherIntValue != null)
      {
        anotherIntValue = (int)builder.anotherIntValue;
        hasAnotherIntValue = true;
      }
      else
      {
        anotherIntValue = 2;
        hasAnotherIntValue = false;
      }
    }

  }

  public class SimpleRecordBuilder
  {
    public string stringField { get; set; }
    public int? intValue { get; set; }
    public int? anotherIntValue { get; set; }

    public SimpleRecord Build()
    {
      return new SimpleRecord(this);
    }
  }
}