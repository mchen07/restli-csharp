using System.Collections.Generic;
using System;


// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp

namespace SimpleNestedArray
{

    ///<summary>Test simple nested array</summary>
  public class SimpleNestedArray
  {

    // required
    public List<List<int>> IntArray { get; }


    public SimpleNestedArray(Dictionary<string, object> data)
    {
      object value;
      bool status;
      // Retrieve data for IntArray
      status = data.TryGetValue("IntArray", out value);
      if (status)
      {

        IntArray = (List<List<int>>)value;
      }

    }

    public SimpleNestedArray(Builder builder)
    {
      // Retrieve data for IntArray
      if (builder.IntArray != null)
      {

        IntArray = (List<List<int>>)builder.IntArray;
      }
      else
      {
        throw new System.ArgumentNullException("Required field with no default must be included in builder: IntArray");
      }
    }
  }

  public class Builder
  {
    public List<List<int>> IntArray { get; set; }

    public SimpleNestedArray build()
    {
      return new SimpleNestedArray(this);
    }
  }
}