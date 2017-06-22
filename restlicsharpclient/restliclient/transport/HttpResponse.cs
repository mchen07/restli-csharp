using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpclient.restliclient.request;

namespace restlicsharpclient.restliclient.transport
{
    public class HttpResponse
    {
        public int? status { get; }
        public Dictionary<string, string> headers { get; }
        public byte[] data { get; }


        public HttpResponse(int? status, Dictionary<string, string> headers, byte[] data)
        {
            this.status = status;
            this.headers = headers;
            this.data = data;
        }
    }
}
