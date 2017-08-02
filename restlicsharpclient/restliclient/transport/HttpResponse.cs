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

using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// Low-level representation of an HTTP response that is constructed by the TransportClient
    /// after receiving a server response.
    /// <para>Stores entity data in the form of a byte array.</para>
    /// </summary>
    public class HttpResponse
    {
        public int? status { get; }
        public IReadOnlyDictionary<string, string> headers { get; }
        public byte[] data { get; }
        public RestliException error;


        public HttpResponse(int? status, Dictionary<string, string> headers, byte[] data, RestliException error)
        {
            this.status = status;
            this.headers = headers ?? new Dictionary<string, string>();
            this.data = data;
            
            if (error != null)
            {
                this.error = error;
            }
            else
            {
                AddStatusOrHeaderError();
            }
        }

        public HttpResponse(HttpWebResponse httpWebResponse)
        {
            byte[] dataBytes = httpWebResponse.GetResponseStream().ReadAllBytes();

            Dictionary<string, string> tempHeaders = new Dictionary<string, string>();
            WebHeaderCollection responseHeaders = httpWebResponse.Headers;
            string[] responseHeaderKeys = responseHeaders.AllKeys;
            foreach (string key in responseHeaderKeys)
            {
                tempHeaders.Add(key, responseHeaders.Get(key));
            }

            // Set all class fields
            status = (int)httpWebResponse.StatusCode;
            headers = tempHeaders;
            data = dataBytes;

            AddStatusOrHeaderError();
        }

        public HttpResponse(HttpResponseMessage httpResponseMessage)
        {
            Dictionary<string, string> tempHeaders = new Dictionary<string, string>();
            HttpResponseHeaders responseHeaders = httpResponseMessage.Headers;
            foreach (KeyValuePair<string, IEnumerable<string>> header in responseHeaders)
            {
                tempHeaders.Add(header.Key, String.Join(RestConstants.kHeaderDelimiter, header.Value));
            }

            // Set all class fields
            status = (int)httpResponseMessage.StatusCode;
            headers = tempHeaders;
            data = httpResponseMessage.Content.ReadAsByteArrayAsync().Result;

            AddStatusOrHeaderError();
        }

        public bool hasError()
        {
            return error != null;
        }

        private void AddStatusOrHeaderError()
        {
            if (headers.ContainsKey(RestConstants.kHeaderRestliErrorResponse))
            {
                error = new RestliException("Server returned Rest.li error response", null);
            }
            else if (status < 200 || status >= 300)
            {
                error = new RestliException(String.Format("Response has HTTP status code {0}", status), null);
            }
        }
    }
}
