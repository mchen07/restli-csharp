using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpclient.restliclient.transport;
using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient
{
    public class RestClient
    {
        public string urlPrefix;
        public TransportClient transportClient;
        // Timeout?

        public RestClient(TransportClient transportClient, string urlPrefix)
        {
            this.transportClient = transportClient;
            this.urlPrefix = urlPrefix;
        }

        public RestClient(string urlPrefix)
        {
            this.transportClient = new DefaultTransportClient();
            this.urlPrefix = urlPrefix;
        }

        public TResponse RestRequestSync<TResponse>(Request request, RestResponseDecoder<TResponse> decoder) where TResponse : Response
        {
            HttpRequest httpRequest = ClientUtil.BuildHttpRequest(request, urlPrefix);
            HttpResponse httpResponse = transportClient.RestRequestSync(httpRequest);
            TransportResponse transportResponse = new TransportResponse(httpResponse);
            return decoder.DecodeResponse(transportResponse);
        }
    }
}
