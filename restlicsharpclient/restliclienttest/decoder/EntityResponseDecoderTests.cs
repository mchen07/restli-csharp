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
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.transport;

namespace restlicsharpclient.restliclienttest.decoder
{
    [TestClass]
    public class EntityResponseDecoderTests
    {
        [TestMethod]
        public void DecodeResponse()
        {

            EntityResponseDecoder<Greeting> decoder = new EntityResponseDecoder<Greeting>();

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "id", 123 },
                { "message", "hello" },
                { "tone", "FRIENDLY" }
            };
            TransportResponse transportResponse = new TransportResponse(data, null);

            EntityResponse<Greeting> response = decoder.DecodeResponse(transportResponse);

            Greeting g = response.element;

            Assert.IsTrue(g.hasId);
            Assert.IsTrue(g.hasMessage);
            Assert.IsTrue(g.hasTone);

            Assert.AreEqual(123, g.id);
            Assert.AreEqual("hello", g.message);
            Assert.AreEqual(Tone.Symbol.FRIENDLY, g.tone.symbol);
        }
    }
}