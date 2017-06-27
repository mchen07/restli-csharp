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
using System.IO;
using System.Net;

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// Default class to be used by the Rest Client if no other TransportClient is specified.
    /// </summary>
    public class DefaultTransportClient : TransportClient
    {
        //TODO: Implement asynchronous request method

        public HttpResponse RestRequestSync(HttpRequest httpRequest)
        {
            WebRequest webRequest = WebRequest.Create(httpRequest.url);
            webRequest.Method = httpRequest.httpMethod.ToString();
            if (httpRequest.entityBody != null)
            {
                // TODO: Support for retrieving entitiy body
            }

            WebResponse webResponse;
            HttpWebResponse httpWebResponse;
            try
            {
                webResponse = webRequest.GetResponse();
                httpWebResponse = (HttpWebResponse)webResponse;
            }
            catch
            {
                throw new WebException(String.Format("Attempted request: {0} {1}", httpRequest.httpMethod, httpRequest.url));
            }

            byte[] dataBytes = httpWebResponse.GetResponseStream().ReadAllBytes();

            Dictionary<string, string> tempHeaders = new Dictionary<string, string>();
            WebHeaderCollection responseHeaders = httpWebResponse.Headers;
            string[] responseHeaderKeys = responseHeaders.AllKeys;
            foreach (string key in responseHeaderKeys)
            {
                tempHeaders.Add(key, responseHeaders.Get(key));
            }

            HttpResponse httpResponse = new HttpResponse((int)httpWebResponse.StatusCode, tempHeaders, dataBytes);

            return httpResponse;
        }
    }
}
