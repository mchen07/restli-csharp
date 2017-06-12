using System.Collections.Generic;
using System;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com/linkedin/restli/datagenerator/integration/NestedUnion.pdsc

namespace com.linkedin.restli.datagenerator.integration
{
  /// <summary>
  /// Testing union nested inside Array or DataMap for C#.
  /// Also testing reserved names.
  /// </summary>
  public class NestedUnion
  {

    // required
    public IReadOnlyList<UnionInArray> unionInArray { get; }


    public NestedUnion(Dictionary<string, object> data)
    {
      object value;
      // Retrieve data for unionInArray
      if (data.TryGetValue("unionInArray", out value))
      {
        List<Dictionary<string, object>> data0 = (List<Dictionary<string, object>>)value;
        List<UnionInArray> result0 = new List<UnionInArray>();
        foreach (Dictionary<string, object> data1 in data0)
        {
          UnionInArray result1;
          result1 = new UnionInArray(data1);
          result0.Add(result1);
        }
        unionInArray = result0;

      }

    }

    public NestedUnion(NestedUnionBuilder builder)
    {
      // Retrieve data for unionInArray
      if (builder.unionInArray != null)
      {
        unionInArray = (IReadOnlyList<UnionInArray>)builder.unionInArray;

      }
      else
      {
        throw new System.ArgumentNullException("Required field with no default must be included in builder: unionInArray");
      }
    }

    public class UnionInArray
    {
      public string asString { get; }
      public int? asInt { get; }
      public Mode? dataMode { get; }
    
      public enum Mode
      {
        String,
        Int,
        NULL
      }
    
      public UnionInArray(Dictionary<string, object> dataMap)
      {
        foreach (KeyValuePair<string, object> dataPair in dataMap)
        {
          if (dataPair.Key.Equals("string"))
          {
            asString = (string)dataPair.Value;
            dataMode = Mode.String;
            break;
          }
          if (dataPair.Key.Equals("int"))
          {
            asInt = (int)dataPair.Value;
            dataMode = Mode.Int;
            break;
          }
        }
        throw new System.ArgumentNullException("Unable to find argument of valid type in union constructor: UnionInArray");
      }
    
    
      public UnionInArray(string value)
      {
        asString = value;
        dataMode = Mode.String;
      }
    
      public UnionInArray(int value)
      {
        asInt = value;
        dataMode = Mode.Int;
      }
    }
  }

  public class NestedUnionBuilder
  {
    public IReadOnlyList<NestedUnion.UnionInArray> unionInArray { get; set; }

    public NestedUnion Build()
    {
      return new NestedUnion(this);
    }
  }
}