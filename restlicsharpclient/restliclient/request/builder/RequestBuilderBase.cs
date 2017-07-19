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

using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient.request.builder
{
    /// <summary>
    /// Base class for request builder classes that are used to construct rest requests.
    /// </summary>
    /// <typeparam name="TRequest">The type of Request being constructed</typeparam>
    /// <typeparam name="TResponse">The type of Response expected</typeparam>
    public abstract class RequestBuilderBase<TRequest, TResponse> where TRequest : Request<TResponse> where TResponse : Response
    {
        public Dictionary<string, List<string>> headers;
        public Dictionary<string, object> queryParams;
        public string baseUrlTemplate { get; }
        public Dictionary<string, object> pathKeys;

        public RequestBuilderBase(string baseUrlTemplate)
        {
            headers = new Dictionary<string, List<string>>();
            queryParams = new Dictionary<string, object>();
            this.baseUrlTemplate = baseUrlTemplate;
            pathKeys = new Dictionary<string, object>();
        }

        public abstract TRequest Build();

        public void AddHeader(string key, string value)
        {
            if (headers.ContainsKey(key))
            {
                if (headers[key] == null)
                {
                    headers[key] = new List<string>();
                }
            }
            else
            {
                headers.Add(key, new List<string>());
            }
            headers[key].Add(value);
        }

        public void PathKey(string key, object value)
        {
            pathKeys.Add(key, value);
        }
    }
}