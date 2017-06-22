using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpclient.restliclient.request;

namespace restlicsharpclient.restliclient.util
{
    class RequestUrlBuilder
    {
        private Request request;
        private string urlPrefix;

        public RequestUrlBuilder(Request request, string urlPrefix)
        {
            this.request = request;
            this.urlPrefix = urlPrefix;
        }

        public string Build()
        {
            string url = urlPrefix + request.baseUrlTemplate;

            dynamic requestKey = request.GetRequestKey();
            if (requestKey != null)
            {
                url = AppendKeyToPath(url, requestKey);
            }

            url = AppendQueryParams(url);
            return url;
        }

        public string AppendKeyToPath(string url, dynamic requestKey)
        {
            return String.Format("{0}/{1}", url, requestKey.ToString());
        }

        public string AppendQueryParams(string url)
        {
            Dictionary<string, object> queryParams = request.queryParams;

            List<string> encodedQueryItems = new List<string>();
            foreach (KeyValuePair<string, object> pair in queryParams)
            {
                // Currently not encoded. To be implemented later.
                encodedQueryItems.Add(String.Format("{0}={1}", pair.Key, pair.Value.ToString())); 
            }

            return String.Format("{0}?{1}", url, String.Join("&", encodedQueryItems));
        }
    }
}
