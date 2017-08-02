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

using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;

namespace restlicsharpclient.restliclient.transport
{
    /// <summary>
    /// Implementation of TransportCallback that decodes the received response
    /// and passes the resulting repsonse into a specified Rest.li callback.
    /// </summary>
    /// <typeparam name="TResponse">The type of Response handled</typeparam>
    class RestliCallbackAdapter<TResponse> : TransportCallback
        where TResponse : Response
    {
        private RestResponseDecoder<TResponse> responseDecoder;
        private RestliCallback<TResponse> callback;

        public RestliCallbackAdapter(RestResponseDecoder<TResponse> responseDecoder, RestliCallback<TResponse> callback)
        {
            this.responseDecoder = responseDecoder;
            this.callback = callback;
        }

        public void OnSuccess(HttpResponse httpResponse)
        {
            TransportResponse transportResponse = new TransportResponse(httpResponse);
            TResponse response = responseDecoder.DecodeResponse(transportResponse);
            callback.OnSuccess(response);
        }

        public void OnError(HttpResponse httpResponse)
        {
            TransportResponse transportResponse = new TransportResponse(httpResponse);
            ClientErrorResponse clientErrorResponse = transportResponse.getError();
            callback.OnError(clientErrorResponse);
        }
    }
}
