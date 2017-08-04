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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using com.linkedin.restli.common;
using restlicsharpclient.restliclient;
using restlicsharpclient.restliclient.util;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.transport;

namespace restlicsharpclient.restliclienttest.decoder
{
    [TestClass]
    public class ErrorResponseDecoderTests
    {
        [TestMethod]
        public void DecodeResponse()
        {
            ErrorResponseDecoder decoder = new ErrorResponseDecoder();

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "exceptionClass", "com.linkedin.restli.server.RestLiServiceException" },
                { "message", "Example message" },
                { "stackTrace", "Example stack trace" },
                { "status", 400 }
            };

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { RestConstants.kHeaderRestliErrorResponse, "true" }
            };

            RestliException exception = new RestliException("Server returned Rest.li error response", null);
            HttpResponse httpResponse = new HttpResponse(RestConstants.httpStatusInternalServerError, headers, null, exception);
            TransportResponse transportResponse = new TransportResponse(data, httpResponse);

            ClientErrorResponse response = decoder.DecodeResponse(transportResponse);

            ErrorResponse error = response.error.details;

            Assert.IsNotNull(error);

            Assert.IsTrue(error.hasExceptionClass);
            Assert.IsTrue(error.hasMessage);
            Assert.IsTrue(error.hasStackTrace);
            Assert.IsTrue(error.hasStatus);

            Assert.AreEqual("com.linkedin.restli.server.RestLiServiceException", error.exceptionClass);
            Assert.AreEqual("Example message", error.message);
            Assert.AreEqual("Example stack trace", error.stackTrace);
            Assert.AreEqual(400, error.status);
        }
    }
}