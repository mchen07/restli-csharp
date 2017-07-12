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

namespace restlicsharpclient.restliclient.util
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

        public string Build()
        {
            dynamic requestKey = request.GetRequestKey();
            return String.Format("{0}{1}{2}{3}", urlPrefix, request.baseUrlTemplate, GetEncodedRequestKey(requestKey), GetEncodedQueryParams(request.queryParams));
        }

        public string GetEncodedRequestKey(dynamic requestKey)
        {
            // Returns "/{requestKey}" if requestKey is not null
            return requestKey == null ? "" : String.Format("{0}{1}", UrlConstants.kPathSep, UrlParamUtil.EncodeDataObject(requestKey));
        }

        public string GetEncodedQueryParams(IReadOnlyDictionary<string, object> queryParams)
        {
            // Returns "?{key1}={value1}&...&{keyN}={valueN}" if queryParams isn't empty
            if (request.queryParams.Count > 0)
            {
                return UrlParamUtil.EncodeQueryParams(queryParams.ToDictionary(_ => _.Key, _ => _.Value));
            }
            return "";
        }
    }
}
