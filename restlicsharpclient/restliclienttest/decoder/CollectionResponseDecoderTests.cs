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
using System.IO;

using com.linkedin.restli.test.api;
using com.linkedin.restli.common;
using restlicsharpclient.restliclient.util;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.transport;

namespace restlicsharpclient.restliclienttest.decoder
{
    [TestClass]
    public class CollectionResponseDecoderTests
    {
        [TestMethod]
        public void DecodeResponse_NoPaging()
        {
            CollectionResponseDecoder<Greeting, EmptyRecord> decoder = new CollectionResponseDecoder<Greeting, EmptyRecord>();

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {
                    "elements", new List<object>()
                    {
                        new Dictionary<string, object>()
                        {
                            { "id", 123 },
                            { "message", "hello" },
                            { "tone", "FRIENDLY" }
                        },
                        new Dictionary<string, object>()
                        {
                            { "id", 234 },
                            { "message", "world" },
                            { "tone", "SINCERE" }
                        }
                    }
                }
            };

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "foo", "bar" }
            };

            HttpResponse httpResponse = new HttpResponse(200, headers, null, null);
            TransportResponse transportResponse = new TransportResponse(data, httpResponse);

            CollectionResponse<Greeting, EmptyRecord> response = decoder.DecodeResponse(transportResponse);

            Assert.AreEqual(2, response.elements.Count);
            Assert.AreEqual("bar", response.headers["foo"][0]);

            Assert.IsNull(response.paging);
            Assert.IsNull(response.metadata);

            Assert.AreEqual(123, response.elements[0].id);
            Assert.AreEqual("hello", response.elements[0].message);
            Assert.AreEqual(Tone.Symbol.FRIENDLY, response.elements[0].tone.symbol);

            Assert.AreEqual(234, response.elements[1].id);
            Assert.AreEqual("world", response.elements[1].message);
            Assert.AreEqual(Tone.Symbol.SINCERE, response.elements[1].tone.symbol);
        }

        [TestMethod]
        public void DecodeResponse()
        {
            CollectionResponseDecoder<Greeting, EmptyRecord> decoder = new CollectionResponseDecoder<Greeting, EmptyRecord>();

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {
                    "elements", new List<object>()
                    {
                        new Dictionary<string, object>()
                        {
                            { "id", 123 },
                            { "message", "hello" },
                            { "tone", "FRIENDLY" }
                        },
                        new Dictionary<string, object>()
                        {
                            { "id", 234 },
                            { "message", "world" },
                            { "tone", "SINCERE" }
                        }
                    }
                },
                {
                    "paging", new Dictionary<string, object>()
                    {
                        { "count", 0 },
                        { "start", 0 },
                        { "links", new List<object>() { } }
                    }
                }
            };

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "foo", "bar" }
            };

            HttpResponse httpResponse = new HttpResponse(200, headers, null, null);
            TransportResponse transportResponse = new TransportResponse(data, httpResponse);

            CollectionResponse<Greeting, EmptyRecord> response = decoder.DecodeResponse(transportResponse);

            Assert.AreEqual(2, response.elements.Count);
            Assert.AreEqual("bar", response.headers["foo"][0]);

            Assert.IsNotNull(response.paging);
            Assert.IsNull(response.metadata);

            Assert.AreEqual(123, response.elements[0].id);
            Assert.AreEqual("hello", response.elements[0].message);
            Assert.AreEqual(Tone.Symbol.FRIENDLY, response.elements[0].tone.symbol);

            Assert.AreEqual(234, response.elements[1].id);
            Assert.AreEqual("world", response.elements[1].message);
            Assert.AreEqual(Tone.Symbol.SINCERE, response.elements[1].tone.symbol);
        }

        [TestMethod]
        public void DecodeResponse_WithMetadata()
        {
            CollectionResponseDecoder<Greeting, Greeting> decoder = new CollectionResponseDecoder<Greeting, Greeting>();

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {
                    "elements", new List<object>()
                    {
                        new Dictionary<string, object>()
                        {
                            { "id", 123 },
                            { "message", "hello" },
                            { "tone", "FRIENDLY" }
                        },
                        new Dictionary<string, object>()
                        {
                            { "id", 234 },
                            { "message", "world" },
                            { "tone", "SINCERE" }
                        }
                    }
                },
                {
                    "paging", new Dictionary<string, object>()
                    {
                        { "count", 0 },
                        { "start", 0 },
                        { "links", new List<object>() { } }
                    }
                },
                {
                    "metadata", new Dictionary<string, object>()
                    {
                        { "id", 345 },
                        { "message", "meta" },
                        { "tone", "INSULTING" }
                    }
                }
            };

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "foo", "bar" }
            };

            HttpResponse httpResponse = new HttpResponse(200, headers, null, null);
            TransportResponse transportResponse = new TransportResponse(data, httpResponse);

            CollectionResponse<Greeting, Greeting> response = decoder.DecodeResponse(transportResponse);

            Assert.AreEqual(2, response.elements.Count);
            Assert.AreEqual("bar", response.headers["foo"][0]);

            Assert.IsNotNull(response.paging);
            Assert.IsNotNull(response.metadata);

            Assert.AreEqual(345, response.metadata.id);
            Assert.AreEqual("meta", response.metadata.message);
            Assert.AreEqual(Tone.Symbol.INSULTING, response.metadata.tone.symbol);

            Assert.AreEqual(123, response.elements[0].id);
            Assert.AreEqual("hello", response.elements[0].message);
            Assert.AreEqual(Tone.Symbol.FRIENDLY, response.elements[0].tone.symbol);

            Assert.AreEqual(234, response.elements[1].id);
            Assert.AreEqual("world", response.elements[1].message);
            Assert.AreEqual(Tone.Symbol.SINCERE, response.elements[1].tone.symbol);
        }
    }
}