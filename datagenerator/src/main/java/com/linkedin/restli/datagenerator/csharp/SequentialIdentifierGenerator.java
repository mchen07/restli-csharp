package com.linkedin.restli.datagenerator.csharp;

/**
 * Class used to generate sequential identifiers (e.g. v0, v1, v2, ...)
 * for certain parts of generated C# files, such as in nested LINQ expressions.
 */
public class SequentialIdentifierGenerator
{
  private String base;
  private int seq;

  public SequentialIdentifierGenerator(String base) {
    this.base = base;
    this.seq = 0;
  }

  public String generateIdentifier() {
    return base + seq++;
  }
}