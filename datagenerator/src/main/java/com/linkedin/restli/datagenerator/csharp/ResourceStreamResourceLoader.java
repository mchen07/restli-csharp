/*
   Copyright (c) 2017 LinkedIn Corp.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

package com.linkedin.restli.datagenerator.csharp;

import org.rythmengine.resource.ITemplateResource;
import org.rythmengine.resource.ResourceLoaderBase;


/**
 * @author Evan Williams
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
