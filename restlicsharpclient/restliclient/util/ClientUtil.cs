using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.transport;

namespace restlicsharpclient.restliclient.util
{
    public class ClientUtil
    {
        public static HttpRequest BuildHttpRequest(Request request, string urlPrefix)
        {
            if (request == null)
            {
                return null;
            }
            
            string url = request.GetUrl(urlPrefix);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            foreach (KeyValuePair<string, List<string>> pair in request.headers)
            {
                headers.Add(pair.Key, String.Join(",", pair.Value));
            }

            byte[] requestBody = null; // To be implemented

            HttpRequest httpRequest = new HttpRequest(request.method, url, headers, requestBody);

            return httpRequest;
        }
    }
}
