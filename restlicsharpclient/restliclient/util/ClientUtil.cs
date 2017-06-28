/*
   Copyright (c) 2017 LinkedIn Corp.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.transport;
using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Various utility methods to be used by the Rest Client.
    /// </summary>
    public static class ClientUtil
    {
        /// <summary>
        /// Returns an HttpRequest object constructed from the given Request and a specified URL prefix string.
        /// </summary>
        /// <typeparam name="TResponse">The type of <see cref="Response"/> this request will return</typeparam>
        /// <param name="request">The Request object</param>
        /// <param name="urlPrefix">The URL Prefix (hostname and port)</param>
        /// <returns></returns>
        public static HttpRequest BuildHttpRequest<TResponse>(Request<TResponse> request, string urlPrefix) where TResponse : Response
        {
            if (request == null)
            {
                return null;
            }
            
            string url = request.GetUrl(urlPrefix);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            foreach (KeyValuePair<string, List<string>> pair in request.headers)
            {
                headers.Add(pair.Key, String.Join(RestConstants.kHeaderDelimiter, pair.Value));
            }

            byte[] requestBody = null;
            if (request.input != null)
            {
                using (Stream bodyStream = new MemoryStream())
                using (StreamWriter streamWriter = new StreamWriter(bodyStream))
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
                {
                    JsonSerializer.Create().Serialize(jsonTextWriter, request.input);
                    jsonTextWriter.Flush();
                    bodyStream.Position = 0;
                    requestBody = bodyStream.ReadAllBytes();
                }
            }

            HttpRequest httpRequest = new HttpRequest(GetHttpMethod(request.method), url, headers, requestBody);

            return httpRequest;
        }

        /// <summary>
        /// Maps a Rest.li Resource Method to the corresponding HTTP Method.
        /// </summary>
        /// <param name="resourceMethod">The resource method</param>
        /// <returns>The corresponding HTTP method</returns>
        public static HttpMethod GetHttpMethod(ResourceMethod resourceMethod)
        {
            switch (resourceMethod)
            {
                case ResourceMethod.CREATE:
                    return HttpMethod.POST;
                case ResourceMethod.GET:
                    return HttpMethod.GET;
                default:
                    throw new ArgumentException(String.Format("Unrecognized resource method: {0}", resourceMethod.ToString()));
            }
        }

        public static System.Net.Http.HttpMethod GetHttpMethod(HttpMethod httpMethod)
        {
            switch (httpMethod)
            {
                case HttpMethod.GET:
                    return System.Net.Http.HttpMethod.Get;
                case HttpMethod.POST:
                    return System.Net.Http.HttpMethod.Post;
                default:
                    throw new ArgumentException(String.Format("Unrecognized HTTP method: {0}", httpMethod.ToString()));
            }
        }

        /// <summary>
        /// Extension method to read all bytes from a stream and return them in an array.
        /// </summary>
        /// <param name="stream">Stream of bytes</param>
        /// <returns>Byte array of entire stream data</returns>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            byte[] dataBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                dataBytes = memoryStream.ToArray();
                memoryStream.Close();
            }
            return dataBytes;
        }
    }
}
