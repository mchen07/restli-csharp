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

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// Low-level abstraction of an HTTP request to be used by the TransportClient when making the request.
    /// </summary>
    public class HttpRequest
    {
        public HttpMethod httpMethod { get; }
        public string url { get; }
        public Dictionary<string, string> headers { get; }
        public byte[] entityBody { get; }

        public HttpRequest(HttpMethod httpMethod, string url, Dictionary<string, string> headers, byte[] entityBody)
        {
            this.httpMethod = httpMethod;
            this.url = url;
            this.headers = headers;
            this.entityBody = entityBody;
        }
    }
}
