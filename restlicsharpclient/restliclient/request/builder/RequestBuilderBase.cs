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
using System.Collections;
using System.Linq;

using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient.request.builder
{
    /// <summary>
    /// Base class for request builder classes that are used to construct rest requests.
    /// </summary>
    /// <typeparam name="TRequest">The type of Request being constructed</typeparam>
    /// <typeparam name="TResponse">The type of Response expected</typeparam>
    public abstract class RequestBuilderBase<TRequest, TResponse> 
        where TRequest : Request<TResponse>
        where TResponse : Response
    {
        protected Dictionary<string, List<string>> headers;
        protected Dictionary<string, object> queryParams;
        protected string baseUrlTemplate { get; }
        protected Dictionary<string, object> pathKeys;

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

        public void SetHeader(string key, List<string> value)
        {
            headers[key] = value;
        }

        public void SetHeaders(Dictionary<string, List<string>> headers)
        {
            this.headers = headers.ToDictionary(_ => _.Key, _ => (_.Value as List<string>).ToList());
        }

        public void ClearHeaders()
        {
            headers = new Dictionary<string, List<string>>();
        }

        public List<string> GetHeader(string key)
        {
            return headers.ContainsKey(key) ? headers[key].ToList() : null;
        }

        public void AddParam(string key, object value)
        {
            object existingValue;
            // check that the retrieved value is not null, since C# allows null dictionary values
            if (queryParams.TryGetValue(key, out existingValue) && existingValue != null)
            {
                if (queryParams[key] is List<object>)
                {
                    List<object> newData = (List<object>)queryParams[key];
                    newData.Add(value);
                    SetParam(key, newData);
                }
                // If query parameter is already set to non-collection value, user need to reset it to null
                // before evoking this call, so do nothing here
            }
            else
            {
                SetParam(key, new List<object>() { value });
            }
        }
        
        public void SetParam(string key, object value)
        {
            queryParams[key] = value;
        }

        public void SetParams(Dictionary<string, object> paramMap)
        {
            foreach (KeyValuePair<string, object> pair in paramMap)
            {
                queryParams[pair.Key] = pair.Value;
            }
        }

        public void ClearParam(string key)
        {
            queryParams.Remove(key);
        }

        public void ClearParams()
        {
            queryParams = new Dictionary<string, object>();
        }

        public bool HasParam(string key)
        {
            return queryParams.ContainsKey(key);
        }

        public object GetParam(string key)
        {
            object output;
            return queryParams.TryGetValue(key, out output) ? output : null;
        }

        public void SetPathKey(string key, object value)
        {
            pathKeys[key] = value;
        }

        public void SetPathKeys(Dictionary<string, object> pathKeyMap)
        {
            foreach (KeyValuePair<string, object> pair in pathKeyMap)
            {
                pathKeys[pair.Key] = pair.Value;
            }
        }

        public bool HasPathKey(string key)
        {
            return pathKeys.ContainsKey(key);
        }

        public object GetPathKey(string key)
        {
            object output;
            return pathKeys.TryGetValue(key, out output) ? output : null;
        }
    }
}