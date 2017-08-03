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
using System.IO;
using System.Net;

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// A type of TransportClient that can be used by the Rest Client, uses the C# WebRequest net stack.
    /// <para>Currently only supports HTTP GET requests.</para>
    /// </summary>
    public class WebRequestTransportClient : TransportClient
    {
        private class RequestState
        {
            public HttpWebRequest httpWebRequest;
            public HttpRequest httpRequest;
            public TransportCallback transportCallback;
        }

        public static void GetRequestStreamCallback(IAsyncResult asyncResult)
        {
            RequestState requestState = (RequestState)asyncResult.AsyncState;
            HttpWebRequest httpWebRequest = requestState.httpWebRequest;
            HttpRequest httpRequest = requestState.httpRequest;

            Stream writeStream = httpWebRequest.EndGetRequestStream(asyncResult);
            if (httpRequest.entityBody != null)
            {
                // TODO: Support retrieving entity body
                // writeStream.Write(...);
            }
            writeStream.Close();

            httpWebRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), requestState);
        }

        public static void GetResponseCallback(IAsyncResult asyncResult)
        {
            RequestState requestState = (RequestState)asyncResult.AsyncState;
            HttpWebRequest httpWebRequest = requestState.httpWebRequest;
            TransportCallback transportCallback = requestState.transportCallback;

            HttpResponse httpResponse;
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(asyncResult);
                httpResponse = new HttpResponse(httpWebResponse);
                httpWebResponse.Close();
            }
            catch (Exception e)
            {
                httpResponse = new HttpResponse(RestConstants.httpStatusInternalServerError, null, null, new RestliException("Error issuing asynchronous rest request", e));
            }

            transportCallback.OnSuccess(httpResponse);
        }

        public void RestRequestAsync(HttpRequest httpRequest, TransportCallback transportCallback)
        {
            if (httpRequest.httpMethod != HttpMethod.GET)
            {
                throw new InvalidOperationException("WebRequestTransportClient currently only supports HTTP GET requests.");
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(httpRequest.url);
            httpWebRequest.Method = httpRequest.httpMethod.ToString();
            httpWebRequest.Headers = httpRequest.webHeaderCollection;

            RequestState requestState = new RequestState()
            {
                httpWebRequest = httpWebRequest,
                httpRequest = httpRequest,
                transportCallback = transportCallback
            };

            if (httpRequest.entityBody == null)
            {
                httpWebRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), requestState);
            }
            else
            {
                httpWebRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), requestState);
            }
        }

        public HttpResponse RestRequestSync(HttpRequest httpRequest)
        {
            if (httpRequest.httpMethod != HttpMethod.GET)
            {
                throw new InvalidOperationException("WebRequestTransportClient currently only supports HTTP GET requests.");
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(httpRequest.url);
            httpWebRequest.Method = httpRequest.httpMethod.ToString();
            httpWebRequest.Headers = httpRequest.webHeaderCollection;

            // TODO: If write supported, implementation would go here

            WebResponse webResponse;
            HttpWebResponse httpWebResponse;

            HttpResponse httpResponse;
            try
            {
                webResponse = httpWebRequest.GetResponse();
                httpWebResponse = (HttpWebResponse)webResponse;
                httpResponse = new HttpResponse(httpWebResponse);
            }
            catch (WebException e)
            {
                httpResponse = new HttpResponse(RestConstants.httpStatusInternalServerError, null, null, new RestliException("Error issuing synchronous rest request", e));
            }

            return httpResponse;
        }
    }
}
