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

using restlicsharpclient.restliclient.util;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// Mid-level representation of a Rest.li server response, between Response and HttpResponse.
    /// <para>Stores entity data in the form of a nested C# in-memory data map.</para>
    /// </summary>
    public class TransportResponse
    {
        public Dictionary<string, object> data;
        public IReadOnlyDictionary<string, string> headers;
        public int? status;
        public RestliException error;
        private ErrorResponseDecoder errorResponseDecoder = new ErrorResponseDecoder();

        // Convert comma-separated wire header to app-expected header
        public Dictionary<string, List<string>> responseHeaders
        {
            get
            {
                return headers.ToDictionary(_ => _.Key, _ => _.Value.Split(RestConstants.kHeaderDelimiters).ToList());
            }
        }

        public TransportResponse(HttpResponse response)
        {
            int? httpStatus = null;

            // if response non-null, extract headers, status code, data, and error
            if (response != null)
            {
                headers = response.headers;
                httpStatus = response.status;
                data = DataUtil.BytesToMap(response.data);
                error = response.error;
            }
            else
            {
                headers = new Dictionary<string, string>();
            }

            status = httpStatus;

            AddStatusOrHeaderError();
        }

        public TransportResponse(Dictionary<string, object> data, HttpResponse response)
        {
            this.data = data;

            int? httpStatus = null;

            // if response non-null, extract headers, status code, and error
            if (response != null)
            {
                headers = response.headers;
                httpStatus = response.status;
                error = response.error;
            }
            else
            {
                headers = new Dictionary<string, string>();
            }

            status = httpStatus;

            AddStatusOrHeaderError();
        }

        public bool hasError()
        {
            return error != null;
        }

        public ClientErrorResponse getError()
        {
            return errorResponseDecoder.DecodeResponse(this);
        }

        private void AddStatusOrHeaderError()
        {
            if (headers.ContainsKey(RestConstants.kHeaderRestliErrorResponse))
            {
                error = new RestliException("Server returned Rest.li error response", null);
            }
            else if (status < 200 || status >= 300)
            {
                error = new RestliException(string.Format("Response has HTTP status code {0}", status), null);
            }
        }
    }
}
