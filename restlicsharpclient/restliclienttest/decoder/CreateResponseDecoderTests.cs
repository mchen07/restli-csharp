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

using com.linkedin.restli.test.api;
using com.linkedin.restli.datagenerator.integration;
using restlicsharpclient.restliclient.util;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.transport;

namespace restlicsharpclient.restliclienttest.decoder
{
    [TestClass]
    public class CreateResponseDecoderTests
    {
        [TestMethod]
        public void DecodeResponse_PrimitiveKey()
        {

            CreateResponseDecoder<int, Greeting> decoder = new CreateResponseDecoder<int, Greeting>();

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "foo", "bar" },
                { RestConstants.kHeaderRestliId, "123" }
            };

            HttpResponse httpResponse = new HttpResponse(RestConstants.httpStatusCreated, headers, null, null);
            TransportResponse transportResponse = new TransportResponse(null, httpResponse);

            CreateResponse<int, Greeting> response = decoder.DecodeResponse(transportResponse);
            
            Assert.AreEqual(123, response.key);
            Assert.AreEqual("123", response.headers[RestConstants.kHeaderRestliId][0]);
            Assert.AreEqual("bar", response.headers["foo"][0]);
            Assert.AreEqual(RestConstants.httpStatusCreated, response.status);
        }

        [TestMethod]
        public void DecodeResponse_EnumKey()
        {

            CreateResponseDecoder<TestEnum, Greeting> decoder = new CreateResponseDecoder<TestEnum, Greeting>();

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "foo", "bar" },
                { RestConstants.kHeaderRestliId, "SYMBOL_1" }
            };

            HttpResponse httpResponse = new HttpResponse(RestConstants.httpStatusCreated, headers, null, null);
            TransportResponse transportResponse = new TransportResponse(null, httpResponse);

            CreateResponse<TestEnum, Greeting> response = decoder.DecodeResponse(transportResponse);

            Assert.AreEqual(TestEnum.Symbol.SYMBOL_1, response.key.symbol);
            Assert.AreEqual("SYMBOL_1", response.headers[RestConstants.kHeaderRestliId][0]);
            Assert.AreEqual("bar", response.headers["foo"][0]);
            Assert.AreEqual(RestConstants.httpStatusCreated, response.status);
        }
    }
}