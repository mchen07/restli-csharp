using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpclient.restliclient.request;

namespace restlicsharpclient.restliclient.transport
{
    public class HttpRequest
    {
        public ResourceMethod method { get; }
        public string url { get; }
        public Dictionary<string, string> headers { get; }
        public byte[] entityBody { get; }

        public HttpRequest(ResourceMethod method, string url, Dictionary<string, string> headers, byte[] entityBody)
        {
            this.method = method;
            this.url = url;
            this.headers = headers;
            this.entityBody = entityBody;
        }
    }
}
