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

using System;

using com.linkedin.restli.test.api;
using restlicsharpclient.restliclient;
using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.request.builder;
using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclientintegrationtest
{
    [TestClass]
    public class RestClientIntegrationTests
    {
        [TestMethod]
        public void RestClientIntegration_GetGreeting()
        {
            /*
             * This test makes the assumption that an instance of `restli-integration-test-server`
             * is running at the urlPrefix (hostname and port) specified below.
             */
            string urlPrefix = "http://evwillia-ld1:1338";
            RestClient client = new RestClient(urlPrefix);

            GetRequestBuilder<int, Greeting> requestBuilder = new GetRequestBuilder<int, Greeting>("/basicCollection");
            requestBuilder.SetID(123);
            GetRequest<int, Greeting> request = requestBuilder.Build();

            EntityResponse<Greeting> response = client.RestRequestSync<EntityResponse<Greeting>>(request);

            Greeting greeting = response.element;

            Assert.IsNotNull(greeting);

            Assert.AreEqual(123, greeting.id);
            Assert.AreEqual(Tone.Symbol.SINCERE, greeting.tone.symbol);
            Assert.AreEqual("Hello World!", greeting.message);
        }
    }
}