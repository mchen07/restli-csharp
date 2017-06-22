using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.request;

namespace restlicsharpclient.restliclient.transport
{
    public interface TransportClient
    {
        HttpResponse RestRequestAsync(HttpRequest request);

        HttpResponse RestRequestSync(HttpRequest request); 
    }
}
