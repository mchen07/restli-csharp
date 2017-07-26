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
using System.Collections.Generic;

using restlicsharpclient.restliclient.request.url;
using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.request.builder;
using restlicsharpclient.restliclient.response;
using com.linkedin.restli.test.api;

namespace restlicsharpclient.restliclienttest
{
    [TestClass]
    public class RequestUrlBuilderTests
    {
        const string complexString = " () :', hello123%=&+?/";
        const string complexStringEscapedQuery = "%20%28%29%20%3A%27%2C%20hello123%25%3D%26%2B?/";
        const string complexStringEscapedPath = "%20%28%29%20%3A%27%2C%20hello123%25=&+%3F%2F";

        [TestMethod]
        public void BuildComplexUrl_Get()
        {
            string expected = String.Format("http://testprefix/foo/1/bar/List(foo,2,3)/baz/(deeper:List(found,it,{0}))/biz/123", complexStringEscapedPath);

            GetRequestBuilder<int, Greeting> requestBuilder = new GetRequestBuilder<int, Greeting>("foo/{one}/bar/{list}/baz/{complex}/biz");
            requestBuilder.SetID(123);
            requestBuilder.PathKey("one", 1);
            requestBuilder.PathKey("list", new List<object>() { "foo", 2, 3 });
            requestBuilder.PathKey("complex", new Dictionary<string, object>() { { "deeper", new List<object>() { "found", "it", complexString } } });
            GetRequest<int, Greeting> request = requestBuilder.Build();

            RequestUrlBuilder<EntityResponse<Greeting>> urlBuilder = new RequestUrlBuilder<EntityResponse<Greeting>>(request, "http://testprefix");
            Uri url = urlBuilder.Build();

            Assert.AreEqual(expected, url.AbsoluteUri);
        }

        [TestMethod]
        public void BuildComplexUrl_Create()
        {
            string expected = String.Format("http://testprefix/foo/1/bar/List(foo,2,3)/baz/(deeper:List(found,it,{0}))/biz", complexStringEscapedPath);

            GreetingBuilder greetingBuilder = new GreetingBuilder()
            {
                id = 555,
                tone = new Tone(Tone.Symbol.INSULTING),
                message = "build URl for this CREATE"
            };
            Greeting greeting = greetingBuilder.Build();

            CreateRequestBuilder<int, Greeting> requestBuilder = new CreateRequestBuilder<int, Greeting>("foo/{one}/bar/{list}/baz/{complex}/biz");
            requestBuilder.PathKey("one", 1);
            requestBuilder.PathKey("list", new List<object>() { "foo", 2, 3 });
            requestBuilder.PathKey("complex", new Dictionary<string, object>() { { "deeper", new List<object>() { "found", "it", complexString } } });
            CreateRequest<int, Greeting> request = requestBuilder.Build();

            RequestUrlBuilder<CreateResponse<int, Greeting>> urlBuilder = new RequestUrlBuilder<CreateResponse<int, Greeting>>(request, "http://testprefix");
            Uri url = urlBuilder.Build();

            Assert.AreEqual(expected, url.AbsoluteUri);
        }
    }
}