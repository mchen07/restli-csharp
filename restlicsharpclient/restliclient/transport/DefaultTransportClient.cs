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

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(asyncResult);

            HttpResponse httpResponse = new HttpResponse(httpWebResponse);

            httpWebResponse.Close();

            transportCallback.OnSuccess(httpResponse);
        }

        public void RestRequestAsync(HttpRequest httpRequest, TransportCallback transportCallback)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(httpRequest.url);
            // TODO: Support httpWebRequest.ContentType = httpRequest.contentType;
            httpWebRequest.Method = httpRequest.httpMethod.ToString();

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
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(httpRequest.url);
            httpWebRequest.Method = httpRequest.httpMethod.ToString();
            if (httpRequest.entityBody != null)
            {
                // TODO: Support for retrieving entity body
            }

            WebResponse webResponse;
            HttpWebResponse httpWebResponse;
            try
            {
                webResponse = httpWebRequest.GetResponse();
                httpWebResponse = (HttpWebResponse)webResponse;
            }
            catch
            {
                throw new WebException(String.Format("Attempted request: {0} {1}", httpRequest.httpMethod, httpRequest.url));
            }

            HttpResponse httpResponse = new HttpResponse(httpWebResponse);

            return httpResponse;
        }
    }
}
