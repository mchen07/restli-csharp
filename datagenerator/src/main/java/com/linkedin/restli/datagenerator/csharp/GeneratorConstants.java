package com.linkedin.restli.datagenerator.csharp;

/**
 * @author Evan Williams
 */
public class GeneratorConstants {
  public static final String NEWLINE = System.getProperty("line.separator");

  public enum FieldParseType {
    FROM_DATAMAP,
    FROM_BUILDER
  };
}
