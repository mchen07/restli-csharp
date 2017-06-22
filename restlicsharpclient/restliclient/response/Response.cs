using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restlicsharpclient.restliclient.response
{
    public abstract class Response
    {
        public Dictionary<string, List<string>> headers;
        public int status;
        public bool fromNetwork;

        public Response(Dictionary<string, List<string>> headers, int status, bool fromNetwork = true)
        {
            this.headers = headers;
            this.status = status;
            this.fromNetwork = fromNetwork;
        }
    }
}
