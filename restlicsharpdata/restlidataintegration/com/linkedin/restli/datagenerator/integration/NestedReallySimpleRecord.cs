using System.Collections.Generic;
using System;
using restlicsharpdata.restlidata;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com/linkedin/restli/datagenerator/integration/NestedReallySimpleRecord.pdsc

namespace com.linkedin.restli.datagenerator.integration
{
  
  public class NestedReallySimpleRecord : RecordTemplate
  {

    // optional
    public SimpleRecord simpleRecordValue { get; }


    // optional, has default value
    public IReadOnlyList<SimpleRecord> collectionWithDefault { get; }
    public bool hasCollectionWithDefault { get; }

    // required, has default value
    public IReadOnlyList<IReadOnlyList<IReadOnlyList<SimpleRecord>>> triple { get; }
    public bool hasTriple { get; }

    // optional
    public IReadOnlyDictionary<string, IReadOnlyDictionary<string, SimpleRecordProjection>> dict { get; }


    // optional
    public IReadOnlyList<IReadOnlyDictionary<string, IReadOnlyList<SimpleRecord>>> mixed { get; }


    public NestedReallySimpleRecord(Dictionary<string, object> data)
    {
      object value;
      // Retrieve data for simpleRecordValue
      if (data.TryGetValue("simpleRecordValue", out value))
      {
        
        simpleRecordValue = new SimpleRecord((Dictionary<string, object>)value);

      }

      // Retrieve data for collectionWithDefault
      if (data.TryGetValue("collectionWithDefault", out value))
      {
        List<Dictionary<string, object>> data0 = (List<Dictionary<string, object>>)value;
        List<SimpleRecord> result0 = new List<SimpleRecord>();
        
        foreach (Dictionary<string, object> data1 in data0)
        {
          SimpleRecord result1;
          
          result1 = new SimpleRecord((Dictionary<string, object>)data1);
          result0.Add(result1);
        }
        collectionWithDefault = result0;
        hasCollectionWithDefault = true;
      }
      else
      {
        collectionWithDefault = new List<SimpleRecord>();
        hasCollectionWithDefault = false;
      }
      // Retrieve data for triple
      if (data.TryGetValue("triple", out value))
      {
        List<List<List<Dictionary<string, object>>>> data0 = (List<List<List<Dictionary<string, object>>>>)value;
        List<IReadOnlyList<IReadOnlyList<SimpleRecord>>> result0 = new List<IReadOnlyList<IReadOnlyList<SimpleRecord>>>();
        
        foreach (List<List<Dictionary<string, object>>> data1 in data0)
        {
          List<IReadOnlyList<SimpleRecord>> result1 = new List<IReadOnlyList<SimpleRecord>>();
          
          foreach (List<Dictionary<string, object>> data2 in data1)
          {
            List<SimpleRecord> result2 = new List<SimpleRecord>();
            
            foreach (Dictionary<string, object> data3 in data2)
            {
              SimpleRecord result3;
              
              result3 = new SimpleRecord((Dictionary<string, object>)data3);
              result2.Add(result3);
            }
            result1.Add(result2);
          }
          result0.Add(result1);
        }
        triple = result0;
        hasTriple = true;
      }
      else
      {
        triple = new List<IReadOnlyList<IReadOnlyList<SimpleRecord>>>();
        hasTriple = false;
      }
      // Retrieve data for dict
      if (data.TryGetValue("dict", out value))
      {
        Dictionary<string, Dictionary<string, Dictionary<string, object>>> data0 = (Dictionary<string, Dictionary<string, Dictionary<string, object>>>)value;
        Dictionary<string, IReadOnlyDictionary<string, SimpleRecordProjection>> result0 = new Dictionary<string, IReadOnlyDictionary<string, SimpleRecordProjection>>();
        
        foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, object>>> data1pair in data0)
        {
          Dictionary<string, Dictionary<string, object>> data1 = data1pair.Value;
          Dictionary<string, SimpleRecordProjection> result1 = new Dictionary<string, SimpleRecordProjection>();
          
          foreach (KeyValuePair<string, Dictionary<string, object>> data2pair in data1)
          {
            Dictionary<string, object> data2 = data2pair.Value;
            SimpleRecordProjection result2;
            
            result2 = new SimpleRecordProjection((Dictionary<string, object>)data2);
            result1.Add(data2pair.Key, result2);
          }
          result0.Add(data1pair.Key, result1);
        }
        dict = result0;

      }

      // Retrieve data for mixed
      if (data.TryGetValue("mixed", out value))
      {
        List<Dictionary<string, List<Dictionary<string, object>>>> data0 = (List<Dictionary<string, List<Dictionary<string, object>>>>)value;
        List<IReadOnlyDictionary<string, IReadOnlyList<SimpleRecord>>> result0 = new List<IReadOnlyDictionary<string, IReadOnlyList<SimpleRecord>>>();
        
        foreach (Dictionary<string, List<Dictionary<string, object>>> data1 in data0)
        {
          Dictionary<string, IReadOnlyList<SimpleRecord>> result1 = new Dictionary<string, IReadOnlyList<SimpleRecord>>();
          
          foreach (KeyValuePair<string, List<Dictionary<string, object>>> data2pair in data1)
          {
            List<Dictionary<string, object>> data2 = data2pair.Value;
            List<SimpleRecord> result2 = new List<SimpleRecord>();
            
            foreach (Dictionary<string, object> data3 in data2)
            {
              SimpleRecord result3;
              
              result3 = new SimpleRecord((Dictionary<string, object>)data3);
              result2.Add(result3);
            }
            result1.Add(data2pair.Key, result2);
          }
          result0.Add(result1);
        }
        mixed = result0;

      }

    }

    public NestedReallySimpleRecord(NestedReallySimpleRecordBuilder builder)
    {
      // Retrieve data for simpleRecordValue
      if (builder.simpleRecordValue != null)
      {
        simpleRecordValue = (SimpleRecord)builder.simpleRecordValue;

      }

      // Retrieve data for collectionWithDefault
      if (builder.collectionWithDefault != null)
      {
        collectionWithDefault = (IReadOnlyList<SimpleRecord>)builder.collectionWithDefault;
        hasCollectionWithDefault = true;
      }
      else
      {
        collectionWithDefault = new List<SimpleRecord>();
        hasCollectionWithDefault = false;
      }
      // Retrieve data for triple
      if (builder.triple != null)
      {
        triple = (IReadOnlyList<IReadOnlyList<IReadOnlyList<SimpleRecord>>>)builder.triple;
        hasTriple = true;
      }
      else
      {
        triple = new List<IReadOnlyList<IReadOnlyList<SimpleRecord>>>();
        hasTriple = false;
      }
      // Retrieve data for dict
      if (builder.dict != null)
      {
        dict = (IReadOnlyDictionary<string, IReadOnlyDictionary<string, SimpleRecordProjection>>)builder.dict;

      }

      // Retrieve data for mixed
      if (builder.mixed != null)
      {
        mixed = (IReadOnlyList<IReadOnlyDictionary<string, IReadOnlyList<SimpleRecord>>>)builder.mixed;

      }

    }

  }

  public class NestedReallySimpleRecordBuilder
  {
    public SimpleRecord simpleRecordValue { get; set; }
    public List<SimpleRecord> collectionWithDefault { get; set; }
    public List<List<List<SimpleRecord>>> triple { get; set; }
    public Dictionary<string, Dictionary<string, SimpleRecordProjection>> dict { get; set; }
    public List<Dictionary<string, List<SimpleRecord>>> mixed { get; set; }

    public NestedReallySimpleRecord Build()
    {
      return new NestedReallySimpleRecord(this);
    }
  }
}