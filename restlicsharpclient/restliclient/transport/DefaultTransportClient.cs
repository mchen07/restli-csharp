using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace restlicsharpclient.restliclient.transport
{
    public class DefaultTransportClient : TransportClient
    {
        public HttpResponse RestRequestAsync(HttpRequest httpRequest)
        {
            throw new NotImplementedException("Asynchronous Rest Request not yet implemented.");
        }

        public HttpResponse RestRequestSync(HttpRequest httpRequest)
        {
            WebRequest webRequest = WebRequest.Create(httpRequest.url);
            webRequest.Method = httpRequest.method.ToString();
            if (httpRequest.entityBody != null)
            {
                // Add entity body to request here
            }

            WebResponse webResponse;
            HttpWebResponse httpWebResponse;
            try
            {
                webResponse = webRequest.GetResponse();
                httpWebResponse = (HttpWebResponse)webResponse;
            }
            catch
            {
                throw new WebException(String.Format("Attempted request: {0} {1}", httpRequest.method, httpRequest.url));
            }

            long dataLength = httpWebResponse.ContentLength;
            byte[] dataBytes = new byte[dataLength];
            Stream stream = httpWebResponse.GetResponseStream();
            stream.Read(dataBytes, 0, (int)dataLength);

            Dictionary<string, string> tempHeaders = new Dictionary<string, string>();
            WebHeaderCollection responseHeaders = httpWebResponse.Headers;
            string[] responseHeaderKeys = responseHeaders.AllKeys;
            foreach (string key in responseHeaderKeys)
            {
                tempHeaders.Add(key, responseHeaders.Get(key));
            }

            HttpResponse httpResponse = new HttpResponse((int)httpWebResponse.StatusCode, tempHeaders, dataBytes);

            return httpResponse;
        }
    }
}
