using System;
using System.Collections.Generic;
using System.Linq;

using restlicsharpdata.restlidata;

// DO NOT EDIT - THIS FILE IS GENERATED BY restli-csharp
// Generated from com\linkedin\restli\common\Link.pdsc

namespace com.linkedin.restli.common
{
  /// <summary>
/// A atom:link-inspired link
/// </summary>
  public class Link : RecordTemplate
  {
    // The link relation e.g. 'self' or 'next'
    public string rel { get; }
    public bool hasRel { get; }
    // The link URI
    public string href { get; }
    public bool hasHref { get; }
    // The type (media type) of the resource
    public string type { get; }
    public bool hasType { get; }

    public Link(Dictionary<string, object> data)
    {
      object value;
      // Retrieve data for rel
      if (data.TryGetValue("rel", out value))
      {
        
rel = (string)value;
        hasRel = true;
      }
      else
      {

        hasRel = false;
      }
      // Retrieve data for href
      if (data.TryGetValue("href", out value))
      {
        
href = (string)value;
        hasHref = true;
      }
      else
      {

        hasHref = false;
      }
      // Retrieve data for type
      if (data.TryGetValue("type", out value))
      {
        
type = (string)value;
        hasType = true;
      }
      else
      {

        hasType = false;
      }
    }

    public Link(LinkBuilder builder)
    {
      // Retrieve data for rel
      if (builder.rel != null)
      {
        
rel = builder.rel;
        hasRel = true;
      }
      else
      {
        throw new ArgumentException("Required field with no default must be included in builder: rel");

      }
      // Retrieve data for href
      if (builder.href != null)
      {
        
href = builder.href;
        hasHref = true;
      }
      else
      {
        throw new ArgumentException("Required field with no default must be included in builder: href");

      }
      // Retrieve data for type
      if (builder.type != null)
      {
        
type = builder.type;
        hasType = true;
      }
      else
      {
        throw new ArgumentException("Required field with no default must be included in builder: type");

      }
    }

    public override Dictionary<string, object> Data()
    {
      Dictionary<string, object> dataMap = new Dictionary<string, object>();
      if (hasRel)
        dataMap.Add("rel", rel);
      if (hasHref)
        dataMap.Add("href", href);
      if (hasType)
        dataMap.Add("type", type);
      return dataMap;
    }

  }

  public class LinkBuilder
  {
    public string rel { get; set; }
    public string href { get; set; }
    public string type { get; set; }

    public Link Build()
    {
      return new Link(this);
    }
  }
}
