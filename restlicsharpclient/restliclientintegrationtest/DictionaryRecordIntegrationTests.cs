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
using System.Linq;

using restlicsharpclient.restliclient;
using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.request.builder;
using restlicsharpclient.restliclient.response;
using com.linkedin.restli.common;
using restlicsharpdata.restlidata;

namespace restlicsharpclient.restliclientintegrationtest
{
    [TestClass]
    public class DictionaryRecordIntegrationTests
    {
        [TestMethod]
        public void Finder_Async()
        {
            /*
             * This test makes the assumption that an instance of `restli-integration-test-server`
             * is running at the urlPrefix (hostname and port) specified below.
             */
            string urlPrefix = "http://evwillia-ld1:1338";
            RestClient client = new RestClient(urlPrefix);

            FinderRequestBuilder<DictionaryRecordTemplate, EmptyRecord> requestBuilder = 
                new FinderRequestBuilder<DictionaryRecordTemplate, EmptyRecord>("/basicCollection");
            requestBuilder.Name("search");
            requestBuilder.SetParam("tone", "FRIENDLY");
            FinderRequest<DictionaryRecordTemplate, EmptyRecord> request = requestBuilder.Build();

            CollectionResponse<DictionaryRecordTemplate, EmptyRecord> response = client.RestRequestSync(request);

            IReadOnlyList<DictionaryRecordTemplate> greetings = response.elements;

            Assert.IsTrue(greetings.Count() > 0);
            Assert.IsTrue(greetings.All(_ => _.Data()["message"].Equals("search") && _.Data()["tone"].Equals("FRIENDLY")));
            Assert.AreEqual(10, response.paging.count);
            Assert.AreEqual(0, response.paging.start);
            CollectionAssert.AllItemsAreInstancesOfType(response.paging.links.ToList(), typeof(Link));
            Assert.AreEqual("application/json", response.paging.links[0].type);
        }
    }
}