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

using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclienttest
{
    [TestClass]
    public class UrlParamUtilTests
    {
        const string complexString = " () :', hello123%=&/";
        const string complexStringEscaped = "%20%28%29%20%3A%27%2C%20hello123%25%3D%26%2F";

        [TestMethod]
        public void EncodeString()
        {
            string actual = UrlParamUtil.EncodeString(complexString);

            Assert.AreEqual(complexStringEscaped, actual);            
        }

        [TestMethod]
        public void EncodeDataList()
        {
            string expected = string.Format("List(one,2,3,{0})", complexStringEscaped);
            List<object> input = new List<object>() { "one", 2, 3, complexString };

            string actual = UrlParamUtil.EncodeDataList(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeDataMap()
        {
            string expected = string.Format("(one:foo,two:2,three:{0})", complexStringEscaped);
            Dictionary<string, object> input = new Dictionary<string, object>() {
                { "one", "foo" },
                { "two", 2 },
                { "three", complexString }
            };

            string actual = UrlParamUtil.EncodeDataMap(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeEmptyString()
        {
            const string expected = UrlConstants.kEmptyStrRep;
            const string input = "";

            string actual = UrlParamUtil.EncodeString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeComplexObject()
        {
            string expected = string.Format("(one:foo,two:List(one,2,(zero:{0})),three:{1})", UrlConstants.kEmptyStrRep, complexStringEscaped);
            Dictionary<string, object> input = new Dictionary<string, object>() {
                { "one", "foo" },
                { "two", new List<object>() { "one", 2, new Dictionary<string, object>() { { "zero", "" } } } },
                { "three", complexString }
            };

            string actual = UrlParamUtil.EncodeDataObject(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodeQueryParams()
        {
            string expected = String.Format("?one=1&list=List(foo,2,3)&complex=(deeper:List(found,it,{0}))", complexStringEscaped);
            Dictionary <string, object> queryParams = new Dictionary<string, object>()
            {
                { "one", 1 },
                { "list", new List<object>() { "foo", 2, 3 } },
                { "complex", new Dictionary<string, object>() { { "deeper", new List<object>() { "found", "it", complexString } } } }
            };

            string actual = UrlParamUtil.EncodeQueryParams(queryParams);

            Assert.AreEqual(expected, actual);
        }
    }
}