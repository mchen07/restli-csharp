using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpclient.restliclient.transport;
using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient.response.decoder
{
    public interface RestResponseDecoder<TResponse> where TResponse : Response
    {
        TResponse DecodeResponse(TransportResponse transportResponse);
    }
}
