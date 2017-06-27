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

using System;
using System.Collections.Generic;

using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Used for constructing Rest.li request URLs.
    /// </summary>
    /// <typeparam name="TResponse">The type of Response to be retrieved</typeparam>
    class RequestUrlBuilder<TResponse> where TResponse : Response
    {
        private Request<TResponse> request;
        private string urlPrefix;

        public RequestUrlBuilder(Request<TResponse> request, string urlPrefix)
        {
            this.request = request;
            this.urlPrefix = urlPrefix;
        }

        public string Build()
        {
            string url = urlPrefix + request.baseUrlTemplate;

            dynamic requestKey = request.GetRequestKey();
            if (requestKey != null)
            {
                url = AppendKeyToPath(url, requestKey);
            }

            url = AppendQueryParams(url);
            return url;
        }

        public string AppendKeyToPath(string url, dynamic requestKey)
        {
            return String.Format("{0}/{1}", url, requestKey.ToString());
        }

        public string AppendQueryParams(string url)
        {
            Dictionary<string, object> queryParams = request.queryParams;

            List<string> encodedQueryItems = new List<string>();
            foreach (KeyValuePair<string, object> pair in queryParams)
            {
                // Currently not encoded. To be implemented later.
                encodedQueryItems.Add(String.Format("{0}={1}", pair.Key, pair.Value.ToString())); 
            }

            return String.Format("{0}?{1}", url, String.Join("&", encodedQueryItems));
        }
    }
}
