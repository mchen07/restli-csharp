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

using System.Collections.Generic;

using restlicsharpclient.restliclient.transport;
using restlicsharpclient.restliclient.util;
using com.linkedin.restli.common;

namespace restlicsharpclient.restliclient.response.decoder
{
    /// <summary>
    /// Class for decoding a TransportResponse containing error info returned
    /// by a Rest.li server. Decodes this info into a ClientErrorResponse
    /// and wraps the created ErrorResponse into a RestliException instance.
    /// </summary>
    public class ErrorResponseDecoder : RestResponseDecoder<ClientErrorResponse>
    {
        public ClientErrorResponse DecodeResponse(TransportResponse transportResponse)
        {
            ErrorResponse data = null;
            Dictionary<string, object> dataMap = transportResponse.data;
            if (dataMap != null && transportResponse.headers.ContainsKey(RestConstants.kHeaderRestliErrorResponse))
            {
                data = DataUtil.BuildRecord<ErrorResponse>(dataMap);
                transportResponse.error.details = data;
            }
            return new ClientErrorResponse(transportResponse.responseHeaders, transportResponse.status ?? data?.status ?? RestConstants.httpStatusInternalServerError, transportResponse.error);
        }
    }
}
