﻿/*
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

using restlicsharpclient.restliclient.util.url;
using restlicsharpdata.restlidata;
using com.linkedin.restli.test.api;

namespace restlicsharpclient.restliclienttest
{
    [TestClass]
    public class UrlParamUtilTests
    {
        const string complexString = " () :', hello123%=&+?/";
        const string complexStringEscapedQuery = "%20%28%29%20%3A%27%2C%20hello123%25%3D%26%2B?/";
        const string complexStringEscapedPath = "%20%28%29%20%3A%27%2C%20hello123%25=&+%3F%2F";

        private static Greeting greeting;
        const string greetingEncoded = "(id:123,message:encode%20me!,tone:FRIENDLY)";

        private static Tone tone;
        const string toneEncoded = "FRIENDLY";

        private static Bytes bytes;
        const string bytesEncoded = "%00%01%02%030123ABCD%C2%A1%C2%A2%C2%A3%C2%A4";

        [ClassInitialize]
        public static void SetUpTestGlobals(TestContext testContext)
        {
            GreetingBuilder greetingBuilder = new GreetingBuilder();
            greetingBuilder.id = 123;
            greetingBuilder.message = "encode me!";
            greetingBuilder.tone = new Tone(Tone.Symbol.FRIENDLY);
            greeting = greetingBuilder.Build();

            tone = new Tone(Tone.Symbol.FRIENDLY);

            bytes = new Bytes(new byte[]{ 0, 1, 2, 3,
                48, 49, 50, 51,
                65, 66, 67, 68,
                161, 162, 163, 164 });
        }

        [TestMethod]
        public void EncodeString()
        {
            string actualQuery = UrlParamUtil.EncodeString(complexString, UrlConstants.EncodingContext.Query);
            string actualPath = UrlParamUtil.EncodeString(complexString, UrlConstants.EncodingContext.Path);

            Assert.AreEqual(complexStringEscapedQuery, actualQuery);
            Assert.AreEqual(complexStringEscapedPath, actualPath);
        }

        [TestMethod]
        public void EncodeDataList()
        {
            string expected = string.Format("List(one,2,3,{0})", complexStringEscapedQuery);
            List<object> input = new List<object>() { "one", 2, 3, complexString };

            string actual = UrlParamUtil.EncodeDataList(input, UrlConstants.EncodingContext.Query);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeDataMap()
        {
            string expected = string.Format("(one:foo,three:{0},two:2)", complexStringEscapedPath);
            Dictionary<string, object> input = new Dictionary<string, object>() {
                { "one", "foo" },
                { "two", 2 },
                { "three", complexString }
            };

            string actual = UrlParamUtil.EncodeDataMap(input, UrlConstants.EncodingContext.Path);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeRecord()
        {
            string actual = UrlParamUtil.EncodeDataObject(greeting, UrlConstants.EncodingContext.Query);

            Assert.AreEqual(greetingEncoded, actual);
        }

        [TestMethod]
        public void EncodeEnum()
        {
            string actual = UrlParamUtil.EncodeDataObject(tone, UrlConstants.EncodingContext.Query);

            Assert.AreEqual(toneEncoded, actual);
        }

        [TestMethod]
        public void EncodeBytes()
        {
            string actual = UrlParamUtil.EncodeDataObject(bytes, UrlConstants.EncodingContext.Query);

            Assert.AreEqual(bytesEncoded, actual);
        }

        [TestMethod]
        public void EncodeEmptyString()
        {
            const string expected = UrlConstants.kEmptyStrRep;
            const string input = "";

            string actual = UrlParamUtil.EncodeString(input, UrlConstants.EncodingContext.Query);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeComplexObject()
        {
            string expected = string.Format("(one:foo,three:{1},two:List(one,2,{3},(bytes:{4},record:{2},zero:{0})))", UrlConstants.kEmptyStrRep, complexStringEscapedPath, greetingEncoded, toneEncoded, bytesEncoded);
            Dictionary<string, object> input = new Dictionary<string, object>() {
                { "one", "foo" },
                { "two", new List<object>() { "one", 2, tone, new Dictionary<string, object>() { { "zero", "" }, { "record", greeting }, { "bytes", bytes } } } },
                { "three", complexString }
            };

            string actual = UrlParamUtil.EncodeDataObject(input, UrlConstants.EncodingContext.Path);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeQueryParams()
        {
            string expected = String.Format("?complex=(deeper:List(found,it,{0}))&list=List(foo,2,3)&one=1", complexStringEscapedQuery);
            Dictionary <string, object> queryParams = new Dictionary<string, object>()
            {
                { "one", 1 },
                { "list", new List<object>() { "foo", 2, 3 } },
                { "complex", new Dictionary<string, object>() { { "deeper", new List<object>() { "found", "it", complexString } } } }
            };

            string actual = UrlParamUtil.EncodeQueryParams(queryParams);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BindPathKeys()
        {
            Dictionary<string, string> expected = new Dictionary<string, string>()
            {
                { "one", "1" },
                { "list", "List(foo,2,3)" },
                { "complex", String.Format("(deeper:List(found,it,{0}))", complexStringEscapedPath) }
            };

            IReadOnlyDictionary<string, object> pathKeys = new Dictionary<string, object>()
            {
                { "one", 1 },
                { "list", new List<object>() { "foo", 2, 3 } },
                { "complex", new Dictionary<string, object>() { { "deeper", new List<object>() { "found", "it", complexString } } } }
            };

            Dictionary<string, string> actual = UrlParamUtil.EncodePathKeysForUrl(pathKeys);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}