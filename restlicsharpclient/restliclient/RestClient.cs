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

using restlicsharpclient.restliclient.transport;
using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient
{
    /// <summary>
    /// Highest abstracted class to be used for making synchronous and asynchronous rest calls.
    /// <para>Takes a URL prefix (hostname + port) and Transport Client as parameters, with the
    /// provided DefaultTransportClient as the default if none specified.</para>
    /// </summary>
    public class RestClient
    {
        public string urlPrefix;
        public TransportClient transportClient;
        // TODO: Timeout support

        public RestClient(TransportClient transportClient, string urlPrefix)
        {
            this.transportClient = transportClient;
            this.urlPrefix = urlPrefix;
        }

        public RestClient(string urlPrefix)
        {
            transportClient = new DefaultTransportClient();
            this.urlPrefix = urlPrefix;
        }

        public TResponse RestRequestSync<TResponse>(Request<TResponse> request) where TResponse : Response
        {
            HttpRequest httpRequest = ClientUtil.BuildHttpRequest(request, urlPrefix);
            HttpResponse httpResponse = transportClient.RestRequestSync(httpRequest);
            TransportResponse transportResponse = new TransportResponse(httpResponse);
            return request.responseDecoder.DecodeResponse(transportResponse);
        }
    }
}
