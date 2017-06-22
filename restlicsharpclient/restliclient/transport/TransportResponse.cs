using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.transport
{
    public class TransportResponse
    {
        public Dictionary<string, object> data;
        public Dictionary<string, string> headers;
        public int? status;
        // public Error error;
        public bool fromNetwork;
        // public errorDecoder = new ErrorResponseDecoder();

        // Convert comma-separated wire header to app-expected header
        public Dictionary<string, List<string>> responseHeaders
        {
            get
            {
                Dictionary<string, List<string>> appHeaders = new Dictionary<string, List<string>>();
                foreach (KeyValuePair<string, string> pair in headers)
                {
                    appHeaders.Add(pair.Key, pair.Value.Split(RestConstants.kHeaderDelimiters).ToList());
                }
                return appHeaders;
            }
        }

        public TransportResponse(HttpResponse response, bool fromNetwork = true)
        {
            this.fromNetwork = fromNetwork;

            int? httpStatus = null;

            // if response non-null, extract headers, status code, and data
            if (response != null)
            {
                headers = response.headers;
                httpStatus = response.status;
                string dataString = System.Text.Encoding.UTF8.GetString(response.data);

                data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataString, new JsonConverter[] { new DataMapDeserializationConverter() });
            }
            else
            {
                headers = new Dictionary<string, string>();
            }
            

            status = httpStatus;
        }

        public TransportResponse(Dictionary<string, object> data, HttpResponse response, bool fromNetwork = true)
        {
            this.data = data;
            this.fromNetwork = fromNetwork;

            int? httpStatus = null;

            // if response non-null, extract headers and status code
            if (response != null)
            {
                headers = response.headers;
                httpStatus = response.status;
            }
            else
            {
                headers = new Dictionary<string, string>();
            }

            status = httpStatus;
        }

        public TransportResponse(Dictionary<string, object> data, HttpWebResponse response, bool fromNetwork = true)
        {
            this.data = data;
            this.fromNetwork = fromNetwork;

            int? httpStatus = null;

            // if response non-null, extract headers and status code
            if (response != null)
            {
                Dictionary<string, string> tempHeaders = new Dictionary<string, string>();
                WebHeaderCollection responseHeaders = response.Headers;
                string[] responseHeaderKeys = responseHeaders.AllKeys;
                foreach (string key in responseHeaderKeys)
                {
                    tempHeaders.Add(key, responseHeaders.Get(key));
                }
                headers = tempHeaders;
                httpStatus = (int)response.StatusCode;
            }
            else
            {
                headers = new Dictionary<string, string>();
            }

            status = httpStatus;
        }
    }
}
