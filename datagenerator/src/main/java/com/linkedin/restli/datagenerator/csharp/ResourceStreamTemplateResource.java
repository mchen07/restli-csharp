package com.linkedin.restli.datagenerator.csharp;


import java.io.IOException;
import java.net.URL;
import java.net.URLConnection;

import org.apache.commons.io.IOUtils;
import org.rythmengine.resource.TemplateResourceBase;


/**
 * @author Keren Jin
 */
public class ResourceStreamTemplateResource extends TemplateResourceBase {
  private final String _path;
  private URLConnection _connection = null;

  public ResourceStreamTemplateResource(String path, ResourceStreamResourceLoader loader) {
    super(loader);
    _path = path;

    final URL url = getClass().getClassLoader().getResource(path);
    if (url != null) {
      try {
        _connection = url.openConnection();
      } catch (IOException e) {
      }
    }
  }

  @Override
  public Object getKey() {
    return _path;
  }

  @Override
  public boolean isValid() {
    return _connection != null;
  }

  @Override
  protected long defCheckInterval() {
    return 1000 * 5;
  }

  @Override
  protected long lastModified() {
    return _connection.getLastModified();
  }

  @Override
  protected String reload() {
    if (isValid()) {
      try {
        return IOUtils.toString(_connection.getInputStream());
      } catch (IOException e) {
        return null;
      }
    } else {
      return null;
    }
  }
}
