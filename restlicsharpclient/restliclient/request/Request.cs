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
using System.Linq;

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.util;
using restlicsharpclient.restliclient.request.url;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using System;

namespace restlicsharpclient.restliclient.request
{
    /// <summary>
    /// Representation of a Rest.li request.
    /// <para>To be constructed using an instance of the RequestBuilderBase interface.</para>
    /// </summary>
    /// <typeparam name="TResponse">The expected Response type</typeparam>
    public abstract class Request<TResponse> where TResponse : Response
    {
        public readonly ResourceMethod method;
        public readonly RecordTemplate input;
        public IReadOnlyDictionary<string, IReadOnlyList<string>> headers;
        public IReadOnlyDictionary<string, object> queryParams;
        public readonly string baseUrlTemplate;
        public IReadOnlyDictionary<string, object> pathKeys;
        public RestResponseDecoder<TResponse> responseDecoder;


        public Request(ResourceMethod method, RecordTemplate input, Dictionary<string, List<string>> headers, Dictionary<string, object> queryParams, string baseUrlTemplate, Dictionary<string, object> pathKeys)
        {
            this.method = method;
            this.input = input;
            this.headers = headers.ToDictionary(_ => _.Key, _ => (IReadOnlyList<string>)_.Value);
            this.queryParams = queryParams;
            this.baseUrlTemplate = baseUrlTemplate;
            this.pathKeys = pathKeys;
        }

        public virtual dynamic GetRequestKey()
        {
            return null;
        }

        public Uri GetUrl(string urlPrefix)
        {
            RequestUrlBuilder<TResponse> requestUrlBuilder = new RequestUrlBuilder<TResponse>(this, urlPrefix);
            return requestUrlBuilder.Build();
        }
    }
}
