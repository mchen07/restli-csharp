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
using restlicsharpclient.restliclient.request.builder;
using restlicsharpclient.restliclient.request;
using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclienttest
{
    [TestClass]
    public class RequestBuilderBaseTests
    {
        public static string baseUrl = "test";

        [TestMethod]
        public void SetHeader()
        {
            GetRequestBuilder<int, Greeting> b = new GetRequestBuilder<int, Greeting>(baseUrl);
            b.AddHeader("header1", "badvalue1");
            b.SetHeader("header1", new List<string>() { "value1" });
            b.AddHeader("header2", "value2");
            b.AddHeader("header2", "value22");

            CollectionAssert.AreEqual(new List<string>() { "value1" }, b.GetHeader("header1"));
            CollectionAssert.AreEqual(new List<string>() { "value2", "value22" }, b.GetHeader("header2"));
        }

        [TestMethod]
        public void AddHeader()
        {
            GetRequestBuilder<int, Greeting> b = new GetRequestBuilder<int, Greeting>(baseUrl);
            b.AddHeader("header1", "value1");
            b.AddHeader("header2", "value2");
            b.AddHeader("header2", "value22");

            CollectionAssert.AreEqual(new List<string>() { "value1" }, b.GetHeader("header1"));
            CollectionAssert.AreEqual(new List<string>() { "value2", "value22" }, b.GetHeader("header2"));
        }

        [TestMethod]
        public void SetParam()
        {
            GetRequestBuilder<int, Greeting> b = new GetRequestBuilder<int, Greeting>(baseUrl);
            Assert.IsFalse(b.HasParam("param1"));

            b.SetParam("param1", 123);
            Assert.IsTrue(b.HasParam("param1"));
            Assert.AreEqual(123, b.GetParam("param1"));

            b.SetParam("param1", "value1");
            b.SetParam("param2", "value2");
            Assert.AreEqual("value1", b.GetParam("param1"));
            Assert.IsTrue(b.HasParam("param2"));
            Assert.AreEqual("value2", b.GetParam("param2"));

            b.SetParams(new Dictionary<string, object>() { { "param1", "new value1" } });
            Assert.IsTrue(b.HasParam("param1"));
            Assert.AreEqual("new value1", b.GetParam("param1"));
            Assert.IsTrue(b.HasParam("param2"));
            Assert.AreEqual("value2", b.GetParam("param2"));

            b.ClearParams();
            Assert.IsFalse(b.HasParam("param1"));
            Assert.IsFalse(b.HasParam("param2"));
        }

        [TestMethod]
        public void AddParam()
        {
            GetRequestBuilder<int, Greeting> b = new GetRequestBuilder<int, Greeting>(baseUrl);

            b.AddParam("param2", "value2");
            Assert.IsFalse(b.HasParam("param1"));
            Assert.IsTrue(b.HasParam("param2"));

            List<object> paramList = (List<object>)b.GetParam("param2");
            Assert.AreEqual("value2", paramList[0], "value2");

            b.AddParam("param2", 123);
            paramList = (List<object>)b.GetParam("param2");
            Assert.IsTrue(paramList.Count == 2);
            Assert.AreEqual("value2", paramList[0]);
            Assert.AreEqual(123, paramList[1]);
        }

        [TestMethod]
        public void SetPathKey()
        {
            GetRequestBuilder<int, Greeting> b = new GetRequestBuilder<int, Greeting>(baseUrl);
            Tone tone = new Tone(Tone.Symbol.FRIENDLY);
            b.SetPathKey("key1", "keyValue1");
            b.SetPathKey("key2", 123);
            b.SetPathKey("key3", tone);
            Assert.AreEqual("keyValue1", b.GetPathKey("key1"));
            Assert.AreEqual(123, b.GetPathKey("key2"));
            Assert.AreEqual(tone.symbol, ((Tone)b.GetPathKey("key3")).symbol);
        }
    }
}