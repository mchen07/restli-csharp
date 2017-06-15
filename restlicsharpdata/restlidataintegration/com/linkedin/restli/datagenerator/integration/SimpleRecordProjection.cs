using System.Collections.Generic;
using System;
using restlicsharpdata.restlidata;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com/linkedin/restli/datagenerator/integration/SimpleRecordProjection.pdsc

namespace com.linkedin.restli.datagenerator.integration
{
  
  public class SimpleRecordProjection : RecordTemplate
  {

    public string stringField { get; }


    public int intValue { get; }
    public bool hasIntValue { get; }

    public SimpleRecordProjection(Dictionary<string, object> data)
    {
      object value;
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
    }

    public SimpleRecordProjection(SimpleRecordProjectionBuilder builder)
    {
      // Retrieve data for stringField
      if (builder.stringField != null)
      {
        
        stringField = builder.stringField;

      }
      else
      {
        throw new ArgumentException("Required field with no default must be included in builder: stringField");
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
    }

  }

  public class SimpleRecordProjectionBuilder
  {
    public string stringField { get; set; }
    public int? intValue { get; set; }

    public SimpleRecordProjection Build()
    {
      return new SimpleRecordProjection(this);
    }
  }
}