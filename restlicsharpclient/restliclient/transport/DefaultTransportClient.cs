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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// Default class to be used by the Rest Client if no other TransportClient is specified.
    /// </summary>
    public class DefaultTransportClient : TransportClient
    {
        private readonly HttpClient httpClient;

        public DefaultTransportClient()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(RestConstants.kHeaderValueApplicationJson));
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        public void RestRequestAsync(HttpRequest httpRequest, TransportCallback transportCallback)
        {
            HttpRequestMessage httpRequestMessage = ConstructHttpRequestMessage(httpRequest);

            httpClient.SendAsync(httpRequestMessage)
                .ContinueWith(task =>
                    {
                        HttpResponse httpResponse;
                        try
                        {
                            httpResponse = new HttpResponse(task.Result);
                        }
                        catch (Exception e)
                        {
                            httpResponse = new HttpResponse(RestConstants.httpStatusInternalServerError, null, null, new RestliException("Error issuing asynchronous rest request", e));
                        }

                        transportCallback.OnResponse(httpResponse);
                    });
        }

        public HttpResponse RestRequestSync(HttpRequest httpRequest)
        {
            HttpRequestMessage httpRequestMessage = ConstructHttpRequestMessage(httpRequest);

            HttpResponse httpResponse;
            try
            {
                HttpResponseMessage httpResponseMessage = httpClient.SendAsync(httpRequestMessage).Result;
                httpResponse = new HttpResponse(httpResponseMessage);
            }
            catch (Exception e)
            {
                httpResponse = new HttpResponse(RestConstants.httpStatusInternalServerError, null, null, new RestliException("Error issuing asynchronous rest request", e));
            }

            return httpResponse;
        }

        private HttpRequestMessage ConstructHttpRequestMessage(HttpRequest httpRequest)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = httpRequest.url,
                Method = ClientUtil.GetHttpMethod(httpRequest.httpMethod)
            };
            if (httpRequest.entityBody != null)
            {
                httpRequestMessage.Content = new ByteArrayContent(httpRequest.entityBody);
            }
            httpRequestMessage.Headers.Clear();
            foreach (KeyValuePair<string, string> header in httpRequest.headers)
            {
                httpRequestMessage.Headers.Add(header.Key, header.Value);
            }

            return httpRequestMessage;
        }
    }
}
