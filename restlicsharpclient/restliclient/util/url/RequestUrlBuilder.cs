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
using System.Linq;

namespace restlicsharpclient.restliclient.util.url
{
    /// <summary>
    /// Builder class used for constructing Rest.li request URLs.
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

        public Uri Build()
        {
            dynamic requestKey = request.GetRequestKey();

            UriBuilder uriBuilder = new UriBuilder(urlPrefix);
            uriBuilder.Path = GetEncodedPath(requestKey);

            if (request.queryParams.Count > 0)
            {
                uriBuilder.Query = GetEncodedQueryParams(request.queryParams);
            }
            
            return uriBuilder.Uri;
        }
        
        public string GetEncodedPath(object requestKey)
        {
            string boundPath = BindPathKeys();
            
            if (requestKey == null)
            {
                return boundPath;
            }
            else
            {
                return String.Format("{0}{1}{2}", boundPath, UrlConstants.kPathSep, UrlParamUtil.EncodeRequestKeyForPath(requestKey));
            }
        }

        public string BindPathKeys()
        {
            // baseUrlTemplate begins as "foo/{fooID}/bar/{barID}/baz"
            // with baz being the actual resource we're calling.
            string baseUrlTemplate = request.baseUrlTemplate;

            Dictionary<string, string> encodedPathKeys = UrlParamUtil.EncodePathKeysForUrl(request.pathKeys);

            string finalString = baseUrlTemplate;
            foreach (KeyValuePair<string, string> pair in encodedPathKeys)
            {
                string target = String.Format("{0}{1}{2}", UrlConstants.kPathKeyTargetBegin, pair.Key, UrlConstants.kPathKeyTargetEnd);
                finalString = finalString.Replace(target, pair.Value);
            }

            return finalString;
        }

        public string GetEncodedQueryParams(IReadOnlyDictionary<string, object> queryParams)
        {
            // Returns "{key1}={value1}&...&{keyN}={valueN}"
            return UrlParamUtil.EncodeQueryParams(queryParams.ToDictionary(_ => _.Key, _ => _.Value));
        }
    }
}
