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

using System.Collections.Generic;

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.request
{
    public enum ResourceMethod
    {
        GET
    }

    public abstract class Request
    {
        public ResourceMethod method;
        public RecordTemplate input;
        public Dictionary<string, List<string>> headers;
        public Dictionary<string, object> queryParams;
        public string baseUrlTemplate;
        
        public Request(ResourceMethod method, RecordTemplate input, Dictionary<string, List<string>> headers, Dictionary<string, object> queryParams, string baseUrlTemplate)
        {
            this.method = method;
            this.input = input;
            this.headers = headers;
            this.queryParams = queryParams;
            this.baseUrlTemplate = baseUrlTemplate;
        }

        public virtual dynamic GetRequestKey()
        {
            return null;
        }

        public string GetUrl(string urlPrefix)
        {
            RequestUrlBuilder requestUrlBuilder = new RequestUrlBuilder(this, urlPrefix);
            return requestUrlBuilder.Build();
        }
    }
}
