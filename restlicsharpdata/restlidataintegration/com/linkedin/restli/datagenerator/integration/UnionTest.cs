using System.Collections.Generic;
using System;
using restlicsharpdata.restlidata;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com/linkedin/restli/datagenerator/integration/UnionTest.pdsc

namespace com.linkedin.restli.datagenerator.integration
{
  /// <summary>
  /// Test generation of C# bindings for unions
  /// </summary>
  public class UnionTest : RecordTemplate
  {

    public UnionEmpty unionEmpty { get; }


    public UnionWithoutNull unionWithoutNull { get; }


    public UnionWithInline unionWithInline { get; }


    public UnionTest(Dictionary<string, object> data)
    {
      object value;
      // Retrieve data for unionEmpty
      if (data.TryGetValue("unionEmpty", out value))
      {
        
        unionEmpty = new UnionEmpty((Dictionary<string, object>)value);

      }

      // Retrieve data for unionWithoutNull
      if (data.TryGetValue("unionWithoutNull", out value))
      {
        
        unionWithoutNull = new UnionWithoutNull((Dictionary<string, object>)value);

      }

      // Retrieve data for unionWithInline
      if (data.TryGetValue("unionWithInline", out value))
      {
        
        unionWithInline = new UnionWithInline((Dictionary<string, object>)value);

      }

    }

    public UnionTest(UnionTestBuilder builder)
    {
      // Retrieve data for unionEmpty
      if (builder.unionEmpty != null)
      {
        
        unionEmpty = builder.unionEmpty;

      }
      else
      {
        throw new ArgumentException("Required field with no default must be included in builder: unionEmpty");
      }
      // Retrieve data for unionWithoutNull
      if (builder.unionWithoutNull != null)
      {
        
        unionWithoutNull = builder.unionWithoutNull;

      }
      else
      {
        throw new ArgumentException("Required field with no default must be included in builder: unionWithoutNull");
      }
      // Retrieve data for unionWithInline
      if (builder.unionWithInline != null)
      {
        
        unionWithInline = builder.unionWithInline;

      }
      else
      {
        throw new ArgumentException("Required field with no default must be included in builder: unionWithInline");
      }
    }

    
    public class UnionWithInline : UnionTemplate
    {
      public IReadOnlyList<string> asArray { get; }
      public float? asFloat { get; }
      public Bytes asBytes { get; }
      public long? asLong { get; }
      public EnumInUnionTest asEnumInUnionTest { get; }
      public string asString { get; }
      public double? asDouble { get; }
      public IReadOnlyDictionary<string, long> asMap { get; }
      public int? asInt { get; }
      public RecordInUnionTest asRecordInUnionTest { get; }
      public Member member { get; }
    
      public enum Member
      {
        Array,
        Float,
        Bytes,
        Long,
        EnumInUnionTest,
        String,
        Double,
        Map,
        Int,
        RecordInUnionTest,
        UNKNOWN
      }
    
      public UnionWithInline(Dictionary<string, object> dataMap)
      {
        foreach (KeyValuePair<string, object> dataPair in dataMap)
        {
          if (dataPair.Key.Equals("array"))
          {
            List<string> data0 = (List<string>)dataPair.Value;
            List<string> result0 = new List<string>();
            
            
            foreach (string data1 in data0)
            {
              string result1;
              
              result1 = (string)data1;
              result0.Add(result1);
            }
            asArray = result0;
            member = Member.Array;
            return;
          }
          if (dataPair.Key.Equals("float"))
          {
            
            asFloat = (float)dataPair.Value;
            member = Member.Float;
            return;
          }
          if (dataPair.Key.Equals("bytes"))
          {
            
            asBytes = new Bytes(BytesUtil.StringToBytes((string)dataPair.Value));
            member = Member.Bytes;
            return;
          }
          if (dataPair.Key.Equals("long"))
          {
            
            asLong = (long)dataPair.Value;
            member = Member.Long;
            return;
          }
          if (dataPair.Key.Equals("com.linkedin.restli.datagenerator.integration.EnumInUnionTest"))
          {
            
            asEnumInUnionTest = new EnumInUnionTest((string)dataPair.Value);
            member = Member.EnumInUnionTest;
            return;
          }
          if (dataPair.Key.Equals("string"))
          {
            
            asString = (string)dataPair.Value;
            member = Member.String;
            return;
          }
          if (dataPair.Key.Equals("double"))
          {
            
            asDouble = (double)dataPair.Value;
            member = Member.Double;
            return;
          }
          if (dataPair.Key.Equals("map"))
          {
            Dictionary<string, long> data0 = (Dictionary<string, long>)dataPair.Value;
            Dictionary<string, long> result0 = new Dictionary<string, long>();
            
            
            foreach (KeyValuePair<string, long> data1pair in data0)
            {
              long data1 = data1pair.Value;
              long result1;
              
              result1 = (long)data1;
              result0.Add(data1pair.Key, result1);
            }
            asMap = result0;
            member = Member.Map;
            return;
          }
          if (dataPair.Key.Equals("int"))
          {
            
            asInt = (int)dataPair.Value;
            member = Member.Int;
            return;
          }
          if (dataPair.Key.Equals("com.linkedin.restli.datagenerator.integration.RecordInUnionTest"))
          {
            
            asRecordInUnionTest = new RecordInUnionTest((Dictionary<string, object>)dataPair.Value);
            member = Member.RecordInUnionTest;
            return;
          }
        }
        member = Member.UNKNOWN;
      }
    
    
      public UnionWithInline(IReadOnlyList<string> value)
      {
        asArray = value;
        member = Member.Array;
      }
    
      public UnionWithInline(float value)
      {
        asFloat = value;
        member = Member.Float;
      }
    
      public UnionWithInline(Bytes value)
      {
        asBytes = value;
        member = Member.Bytes;
      }
    
      public UnionWithInline(long value)
      {
        asLong = value;
        member = Member.Long;
      }
    
      public UnionWithInline(EnumInUnionTest value)
      {
        asEnumInUnionTest = value;
        member = Member.EnumInUnionTest;
      }
    
      public UnionWithInline(string value)
      {
        asString = value;
        member = Member.String;
      }
    
      public UnionWithInline(double value)
      {
        asDouble = value;
        member = Member.Double;
      }
    
      public UnionWithInline(IReadOnlyDictionary<string, long> value)
      {
        asMap = value;
        member = Member.Map;
      }
    
      public UnionWithInline(int value)
      {
        asInt = value;
        member = Member.Int;
      }
    
      public UnionWithInline(RecordInUnionTest value)
      {
        asRecordInUnionTest = value;
        member = Member.RecordInUnionTest;
      }
    }
    
    public class UnionEmpty : UnionTemplate
    {
    
    
      public UnionEmpty(Dictionary<string, object> dataMap)
      {
    
      }
    
    }
    
    public class UnionWithoutNull : UnionTemplate
    {
      public IReadOnlyList<string> asArray { get; }
      public float? asFloat { get; }
      public Bytes asBytes { get; }
      public long? asLong { get; }
      public SimpleRecord asSimpleRecord { get; }
      public TestEnum asTestEnum { get; }
      public string asString { get; }
      public bool? asBoolean { get; }
      public double? asDouble { get; }
      public IReadOnlyDictionary<string, long> asMap { get; }
      public int? asInt { get; }
      public Member member { get; }
    
      public enum Member
      {
        Array,
        Float,
        Bytes,
        Long,
        SimpleRecord,
        TestEnum,
        String,
        Boolean,
        Double,
        Map,
        Int,
        UNKNOWN
      }
    
      public UnionWithoutNull(Dictionary<string, object> dataMap)
      {
        foreach (KeyValuePair<string, object> dataPair in dataMap)
        {
          if (dataPair.Key.Equals("array"))
          {
            List<string> data0 = (List<string>)dataPair.Value;
            List<string> result0 = new List<string>();
            
            
            foreach (string data1 in data0)
            {
              string result1;
              
              result1 = (string)data1;
              result0.Add(result1);
            }
            asArray = result0;
            member = Member.Array;
            return;
          }
          if (dataPair.Key.Equals("float"))
          {
            
            asFloat = (float)dataPair.Value;
            member = Member.Float;
            return;
          }
          if (dataPair.Key.Equals("bytes"))
          {
            
            asBytes = new Bytes(BytesUtil.StringToBytes((string)dataPair.Value));
            member = Member.Bytes;
            return;
          }
          if (dataPair.Key.Equals("long"))
          {
            
            asLong = (long)dataPair.Value;
            member = Member.Long;
            return;
          }
          if (dataPair.Key.Equals("com.linkedin.restli.datagenerator.integration.SimpleRecord"))
          {
            
            asSimpleRecord = new SimpleRecord((Dictionary<string, object>)dataPair.Value);
            member = Member.SimpleRecord;
            return;
          }
          if (dataPair.Key.Equals("com.linkedin.restli.datagenerator.integration.TestEnum"))
          {
            
            asTestEnum = new TestEnum((string)dataPair.Value);
            member = Member.TestEnum;
            return;
          }
          if (dataPair.Key.Equals("string"))
          {
            
            asString = (string)dataPair.Value;
            member = Member.String;
            return;
          }
          if (dataPair.Key.Equals("boolean"))
          {
            
            asBoolean = (bool)dataPair.Value;
            member = Member.Boolean;
            return;
          }
          if (dataPair.Key.Equals("double"))
          {
            
            asDouble = (double)dataPair.Value;
            member = Member.Double;
            return;
          }
          if (dataPair.Key.Equals("map"))
          {
            Dictionary<string, long> data0 = (Dictionary<string, long>)dataPair.Value;
            Dictionary<string, long> result0 = new Dictionary<string, long>();
            
            
            foreach (KeyValuePair<string, long> data1pair in data0)
            {
              long data1 = data1pair.Value;
              long result1;
              
              result1 = (long)data1;
              result0.Add(data1pair.Key, result1);
            }
            asMap = result0;
            member = Member.Map;
            return;
          }
          if (dataPair.Key.Equals("int"))
          {
            
            asInt = (int)dataPair.Value;
            member = Member.Int;
            return;
          }
        }
        member = Member.UNKNOWN;
      }
    
    
      public UnionWithoutNull(IReadOnlyList<string> value)
      {
        asArray = value;
        member = Member.Array;
      }
    
      public UnionWithoutNull(float value)
      {
        asFloat = value;
        member = Member.Float;
      }
    
      public UnionWithoutNull(Bytes value)
      {
        asBytes = value;
        member = Member.Bytes;
      }
    
      public UnionWithoutNull(long value)
      {
        asLong = value;
        member = Member.Long;
      }
    
      public UnionWithoutNull(SimpleRecord value)
      {
        asSimpleRecord = value;
        member = Member.SimpleRecord;
      }
    
      public UnionWithoutNull(TestEnum value)
      {
        asTestEnum = value;
        member = Member.TestEnum;
      }
    
      public UnionWithoutNull(string value)
      {
        asString = value;
        member = Member.String;
      }
    
      public UnionWithoutNull(bool value)
      {
        asBoolean = value;
        member = Member.Boolean;
      }
    
      public UnionWithoutNull(double value)
      {
        asDouble = value;
        member = Member.Double;
      }
    
      public UnionWithoutNull(IReadOnlyDictionary<string, long> value)
      {
        asMap = value;
        member = Member.Map;
      }
    
      public UnionWithoutNull(int value)
      {
        asInt = value;
        member = Member.Int;
      }
    }
  }

  public class UnionTestBuilder
  {
    public UnionTest.UnionEmpty unionEmpty { get; set; }
    public UnionTest.UnionWithoutNull unionWithoutNull { get; set; }
    public UnionTest.UnionWithInline unionWithInline { get; set; }

    public UnionTest Build()
    {
      return new UnionTest(this);
    }
  }
}