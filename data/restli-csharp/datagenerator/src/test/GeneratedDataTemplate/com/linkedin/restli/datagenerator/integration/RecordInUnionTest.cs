using System.Collections.Generic;
using System;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com/linkedin/restli/datagenerator/integration/UnionTest.pdsc

namespace com.linkedin.restli.datagenerator.integration
{
  
  public class RecordInUnionTest
  {

    // required
    public int a { get; }


    public RecordInUnionTest(Dictionary<string, object> data)
    {
      object value;
      // Retrieve data for a
      if (data.TryGetValue("a", out value))
      {
        a = (int)value;

      }

    }

    public RecordInUnionTest(RecordInUnionTestBuilder builder)
    {
      // Retrieve data for a
      if (builder.a != null)
      {
        a = (int)builder.a;

      }
      else
      {
        throw new System.ArgumentNullException("Required field with no default must be included in builder: a");
      }
    }

  }

  public class RecordInUnionTestBuilder
  {
    public int? a { get; set; }

    public RecordInUnionTest Build()
    {
      return new RecordInUnionTest(this);
    }
  }
}