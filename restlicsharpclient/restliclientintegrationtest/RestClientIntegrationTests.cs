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

using System.Threading;
using System.Collections.Generic;
using System.Linq;

using com.linkedin.restli.test.api;
using com.linkedin.restli.common;
using restlicsharpclient.restliclient;
using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.request.builder;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclientintegrationtest
{
    [TestClass]
    public class RestClientIntegrationTests
    {
        const int asyncTimeoutMillis = 3000;

        [TestMethod]
        public void GetGreeting_Sync()
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

            EntityResponse<Greeting> response = client.RestRequestSync(request);

            Greeting greeting = response.element;

            Assert.IsNotNull(greeting);

            Assert.AreEqual(123, greeting.id);
            Assert.AreEqual(Tone.Symbol.SINCERE, greeting.tone.symbol);
            Assert.AreEqual("Hello World!", greeting.message);
        }

        [TestMethod]
        public void GetGreeting_Async()
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

            AutoResetEvent blocker = new AutoResetEvent(false);

            Greeting greeting = null;

            RestliCallback<EntityResponse<Greeting>>.SuccessHandler successHandler = delegate (EntityResponse<Greeting> response)
            {
                greeting = response.element;
                blocker.Set();
            };
            RestliCallback<EntityResponse<Greeting>> callback = new RestliCallback<EntityResponse<Greeting>>(successHandler);

            client.RestRequestAsync(request, callback);

            blocker.WaitOne(asyncTimeoutMillis);

            Assert.IsNotNull(greeting);

            Assert.AreEqual(123, greeting.id);
            Assert.AreEqual(Tone.Symbol.SINCERE, greeting.tone.symbol);
            Assert.AreEqual("Hello World!", greeting.message);
        }

        [TestMethod]
        public void CreateGreeting_Sync()
        {
            /*
             * This test makes the assumption that an instance of `restli-integration-test-server`
             * is running at the urlPrefix (hostname and port) specified below.
             */
            string urlPrefix = "http://evwillia-ld1:1338";
            RestClient client = new RestClient(urlPrefix);
            GreetingBuilder greetingBuilder = new GreetingBuilder();
            greetingBuilder.id = 0; // Dummy value
            greetingBuilder.message = "Create me!";
            greetingBuilder.tone = new Tone(Tone.Symbol.FRIENDLY);
            Greeting input = greetingBuilder.Build();

            CreateRequestBuilder<int, Greeting> requestBuilder = new CreateRequestBuilder<int, Greeting>("/basicCollection");
            requestBuilder.input = input;
            CreateRequest<int, Greeting> request = requestBuilder.Build();

            CreateResponse<int, Greeting> response = client.RestRequestSync(request);

            Assert.AreEqual(RestConstants.httpStatusCreated, response.status);
            Assert.AreEqual(123, response.key);
            CollectionAssert.AreEqual(new List<string>() { "/basicCollection/123" }, response.headers[RestConstants.kHeaderLocation].ToList());
        }

        [TestMethod]
        public void CreateGreeting_Async()
        {
            /*
             * This test makes the assumption that an instance of `restli-integration-test-server`
             * is running at the urlPrefix (hostname and port) specified below.
             */
            string urlPrefix = "http://evwillia-ld1:1338";
            RestClient client = new RestClient(urlPrefix);
            GreetingBuilder greetingBuilder = new GreetingBuilder();
            greetingBuilder.id = 0; // Dummy value
            greetingBuilder.message = "Create me!";
            greetingBuilder.tone = new Tone(Tone.Symbol.FRIENDLY);
            Greeting input = greetingBuilder.Build();

            CreateRequestBuilder<int, Greeting> requestBuilder = new CreateRequestBuilder<int, Greeting>("/basicCollection");
            requestBuilder.input = input;
            CreateRequest<int, Greeting> request = requestBuilder.Build();

            AutoResetEvent blocker = new AutoResetEvent(false);

            CreateResponse<int, Greeting> createResponse = null;

            RestliCallback<CreateResponse<int, Greeting>>.SuccessHandler successHandler = delegate (CreateResponse<int, Greeting> response)
            {
                createResponse = response;
                blocker.Set();
            };
            RestliCallback<CreateResponse<int, Greeting>> callback = new RestliCallback<CreateResponse<int, Greeting>>(successHandler);
            
            client.RestRequestAsync(request, callback);

            blocker.WaitOne(asyncTimeoutMillis);

            Assert.AreEqual(RestConstants.httpStatusCreated, createResponse.status);
            Assert.AreEqual(123, createResponse.key);
            CollectionAssert.AreEqual(new List<string>() { "/basicCollection/123" }, createResponse.headers[RestConstants.kHeaderLocation].ToList());
        }

        [TestMethod]
        public void FinderGreeting_Sync()
        {
            /*
             * This test makes the assumption that an instance of `restli-integration-test-server`
             * is running at the urlPrefix (hostname and port) specified below.
             */
            string urlPrefix = "http://evwillia-ld1:1338";
            RestClient client = new RestClient(urlPrefix);

            FinderRequestBuilder<Greeting, EmptyRecord> requestBuilder = new FinderRequestBuilder<Greeting, EmptyRecord>("/basicCollection");
            requestBuilder.Name("search");
            requestBuilder.SetParam("tone", Tone.Symbol.FRIENDLY);
            FinderRequest<Greeting, EmptyRecord> request = requestBuilder.Build();

            CollectionResponse<Greeting, EmptyRecord> response = client.RestRequestSync(request);

            IReadOnlyList<Greeting> greetings = response.elements;

            Assert.IsTrue(greetings.Count() > 0);
            Assert.IsTrue(greetings.All(_ => _.message.Equals("search") && _.tone.symbol == Tone.Symbol.FRIENDLY));
            Assert.AreEqual(10, response.paging.count);
            Assert.AreEqual(0, response.paging.start);
            CollectionAssert.AllItemsAreInstancesOfType(response.paging.links.ToList(), typeof(Link));
            Assert.AreEqual("application/json", response.paging.links[0].type);
        }

        [TestMethod]
        public void FinderGreeting_Async()
        {
            /*
             * This test makes the assumption that an instance of `restli-integration-test-server`
             * is running at the urlPrefix (hostname and port) specified below.
             */
            string urlPrefix = "http://evwillia-ld1:1338";
            RestClient client = new RestClient(urlPrefix);

            FinderRequestBuilder<Greeting, EmptyRecord> requestBuilder = new FinderRequestBuilder<Greeting, EmptyRecord>("/basicCollection");
            requestBuilder.Name("search");
            requestBuilder.SetParam("tone", Tone.Symbol.FRIENDLY);
            FinderRequest<Greeting, EmptyRecord> request = requestBuilder.Build();

            AutoResetEvent blocker = new AutoResetEvent(false);

            CollectionResponse<Greeting, EmptyRecord> collectionResponse = null;

            RestliCallback<CollectionResponse<Greeting, EmptyRecord>>.SuccessHandler successHandler = delegate (CollectionResponse<Greeting, EmptyRecord> response)
            {
                collectionResponse = response;
                blocker.Set();
            };
            RestliCallback<CollectionResponse<Greeting, EmptyRecord>> callback = new RestliCallback<CollectionResponse<Greeting, EmptyRecord>>(successHandler);

            client.RestRequestAsync(request, callback);

            blocker.WaitOne(asyncTimeoutMillis);

            IReadOnlyList<Greeting> greetings = collectionResponse.elements;

            Assert.IsTrue(greetings.Count() > 0);
            Assert.IsTrue(greetings.All(_ => _.message.Equals("search") && _.tone.symbol == Tone.Symbol.FRIENDLY));
            Assert.AreEqual(10, collectionResponse.paging.count);
            Assert.AreEqual(0, collectionResponse.paging.start);
            CollectionAssert.AllItemsAreInstancesOfType(collectionResponse.paging.links.ToList(), typeof(Link));
            Assert.AreEqual("application/json", collectionResponse.paging.links[0].type);
        }
    }
}