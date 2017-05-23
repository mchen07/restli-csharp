package com.linkedin.restli.datagenerator.csharp;


import org.rythmengine.resource.ITemplateResource;
import org.rythmengine.resource.ResourceLoaderBase;


/**
 * @author Keren Jin
 */
public class ResourceStreamResourceLoader extends ResourceLoaderBase {
  @Override
  public String getResourceLoaderRoot() {
    return "rythm";
  }

  @Override
  public ITemplateResource load(String path) {
    return new ResourceStreamTemplateResource(path, this);
  }
}
