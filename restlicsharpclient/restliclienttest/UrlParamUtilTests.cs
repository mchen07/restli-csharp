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
using restlicsharpdata.restlidata;
using com.linkedin.restli.test.api;

namespace restlicsharpclient.restliclienttest
{
    [TestClass]
    public class UrlParamUtilTests
    {
        const string complexString = " () :', hello123%=&+?/";
        const string complexEscapedStringQuery = "%20%28%29%20%3A%27%2C%20hello123%25%3D%26%2B?/";
        const string complexEscapedStringPath = "%20%28%29%20%3A%27%2C%20hello123%25=&+%3F%2F";

        private static readonly Dictionary<UrlConstants.EncodingContext, string> complexEscapedStrings = 
            new Dictionary<UrlConstants.EncodingContext, string>()
        {
            { UrlConstants.EncodingContext.Path, complexEscapedStringPath },
            { UrlConstants.EncodingContext.Query, complexEscapedStringQuery },
        };

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
            string input = complexString;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(complexEscapedStrings[encodingContext], actual);
            }
        }

        [TestMethod]
        public void EncodeDataList()
        {
            string expectedTemplate = "List(one,2,3,{0})";
            List<object> input = new List<object>() { "one", 2, 3, complexString };

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(String.Format(expectedTemplate, complexEscapedStrings[encodingContext]), actual);
            }
        }

        [TestMethod]
        public void EncodeDataMap()
        {
            string expectedTemplate = "(one:foo,three:{0},two:2)";
            Dictionary<string, object> input = new Dictionary<string, object>() {
                { "one", "foo" },
                { "two", 2 },
                { "three", complexString }
            };

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(String.Format(expectedTemplate, complexEscapedStrings[encodingContext]), actual);
            }
        }

        [TestMethod]
        public void EncodeRecord()
        {
            string expected = greetingEncoded;
            Greeting input = greeting;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeEnum()
        {
            string expected = toneEncoded;
            Tone input= tone;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeBytes()
        {
            string expected = bytesEncoded;
            Bytes input = bytes;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeInt()
        {
            string expected = "234";
            int input = 234;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeOne()
        {
            string expected = "1";
            int input = 1;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeFloat()
        {
            string expected = "1.23";
            float input = 1.23F;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeDouble()
        {
            string expected = "12.3456789";
            double input = 12.3456789;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeBool()
        {
            string expected = "true";
            bool input = true;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }

            expected = "false";
            input = false;

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeEmptyString()
        {
            const string expected = UrlConstants.kEmptyStrRep;
            const string input = "";

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void EncodeComplexObject()
        {
            string expectedTemplate = string.Format("(one:foo,three:{1},two:List(one,2,{3},(bytes:{4},record:{2},zero:{0})))", UrlConstants.kEmptyStrRep, "{0}", greetingEncoded, toneEncoded, bytesEncoded);
            Dictionary<string, object> input = new Dictionary<string, object>() {
                { "one", "foo" },
                { "two", new List<object>() { "one", 2, tone, new Dictionary<string, object>() { { "zero", "" }, { "record", greeting }, { "bytes", bytes } } } },
                { "three", complexString }
            };

            foreach (UrlConstants.EncodingContext encodingContext in Enum.GetValues(typeof(UrlConstants.EncodingContext)))
            {
                string actual = UrlParamUtil.EncodeDataObject(input, encodingContext);
                Assert.AreEqual(String.Format(expectedTemplate, complexEscapedStrings[encodingContext]), actual);
            }
        }

        [TestMethod]
        public void EncodeQueryParams()
        {
            string expected = String.Format("?complex=(deeper:List(found,it,{0}))&list=List(foo,2,3)&one=1", complexEscapedStringQuery);
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
                { "complex", String.Format("(deeper:List(found,it,{0}))", complexEscapedStringPath) }
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