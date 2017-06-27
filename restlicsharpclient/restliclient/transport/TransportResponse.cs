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
using System.Net;
using Newtonsoft.Json;

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// Mid-level abstraction of a Rest.li server response, between Response and HttpResponse.
    /// <para>Stores entity data in the form of a nested C# in-memory data map.</para>
    /// </summary>
    public class TransportResponse
    {
        public Dictionary<string, object> data;
        public Dictionary<string, string> headers;
        public int? status;
        // TODO: Support for Error object
        // TODO: Support for ErrorResponseDecoder object

        // Convert comma-separated wire header to app-expected header
        public Dictionary<string, List<string>> responseHeaders
        {
            get
            {
                Dictionary<string, List<string>> appHeaders = new Dictionary<string, List<string>>();
                foreach (KeyValuePair<string, string> pair in headers)
                {
                    appHeaders.Add(pair.Key, pair.Value.Split(RestConstants.kHeaderDelimiters).ToList());
                }
                return appHeaders;
            }
        }

        public TransportResponse(HttpResponse response)
        {
            int? httpStatus = null;

            // if response non-null, extract headers, status code, and data
            if (response != null)
            {
                headers = response.headers;
                httpStatus = response.status;
                string dataString = System.Text.Encoding.UTF8.GetString(response.data);

                data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataString, new JsonConverter[] { new DataMapDeserializationConverter() });
            }
            else
            {
                headers = new Dictionary<string, string>();
            }
            

            status = httpStatus;
        }

        public TransportResponse(Dictionary<string, object> data, HttpResponse response)
        {
            this.data = data;

            int? httpStatus = null;

            // if response non-null, extract headers and status code
            if (response != null)
            {
                headers = response.headers;
                httpStatus = response.status;
            }
            else
            {
                headers = new Dictionary<string, string>();
            }

            status = httpStatus;
        }

        public TransportResponse(Dictionary<string, object> data, HttpWebResponse response)
        {
            this.data = data;

            int? httpStatus = null;

            // if response non-null, extract headers and status code
            if (response != null)
            {
                Dictionary<string, string> tempHeaders = new Dictionary<string, string>();
                WebHeaderCollection responseHeaders = response.Headers;
                string[] responseHeaderKeys = responseHeaders.AllKeys;
                foreach (string key in responseHeaderKeys)
                {
                    tempHeaders.Add(key, responseHeaders.Get(key));
                }
                headers = tempHeaders;
                httpStatus = (int)response.StatusCode;
            }
            else
            {
                headers = new Dictionary<string, string>();
            }

            status = httpStatus;
        }
    }
}
