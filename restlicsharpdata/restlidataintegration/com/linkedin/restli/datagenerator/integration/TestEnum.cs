using restlicsharpdata.restlidata;
using System;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com/linkedin/restli/datagenerator/integration/TestEnum.pdsc

namespace com.linkedin.restli.datagenerator.integration
{
  /// <summary>
  /// Doc for the enum
  /// </summary>
  public class TestEnum : EnumTemplate
  {
    public enum Symbol
    {

      // Doc for 1
      SYMBOL_1,

      SYMBOL_2,

      // Doc for 3
      SYMBOL_3,

      @Unknown
    }

    public Symbol symbol { get; }

    public TestEnum(Symbol symbol)
    {
      this.symbol = symbol;
    }

    public TestEnum(string data)
    {
      if (data.Equals("SYMBOL_1"))
      {
        symbol = Symbol.SYMBOL_1;
      }
      else if (data.Equals("SYMBOL_2"))
      {
        symbol = Symbol.SYMBOL_2;
      }
      else if (data.Equals("SYMBOL_3"))
      {
        symbol = Symbol.SYMBOL_3;
      }
      else
      {
        symbol = @Unknown;
      }
    }
  }
}