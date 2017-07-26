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

using com.linkedin.restli.datagenerator.integration;

namespace restlicsharpdata.restlidataintegrationtest
{
    [TestClass]
    public class NestedTypesTests
    {
        readonly static byte[] expectedBytes = new byte[] { 0, 1, 2, 3 };
        const TestEnum.Symbol expectedEnum = TestEnum.Symbol.SYMBOL_1;
        const int expectedInt = 33;
        
        [TestMethod]
        public void DataMap_FullCycle_BytesMap()
        {
            NestedTypes n;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "bytesMap", new Dictionary<string, object>()
                    {
                        { "one", "\u0000\u0001\u0002\u0003" }
                    }
                }
            };

            n = new NestedTypes(data);
            
            Assert.IsTrue(n.hasBytesMap);
            CollectionAssert.AreEqual(expectedBytes, n.bytesMap["one"].GetBytes());

            NestedTypes reclaimed = new NestedTypes(n.Data());

            Assert.IsTrue(reclaimed.hasBytesMap);
            CollectionAssert.AreEqual(expectedBytes, reclaimed.bytesMap["one"].GetBytes());
        }

        [TestMethod]
        public void DataMap_FullCycle_BytesArray()
        {
            NestedTypes n;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "bytesArray", new List<object>()
                    {
                        "\u0000\u0001\u0002\u0003"
                    }
                }
            };

            n = new NestedTypes(data);
            
            Assert.IsTrue(n.hasBytesArray);
            CollectionAssert.AreEqual(expectedBytes, n.bytesArray[0].GetBytes());

            NestedTypes reclaimed = new NestedTypes(n.Data());

            Assert.IsTrue(reclaimed.hasBytesArray);
            CollectionAssert.AreEqual(expectedBytes, reclaimed.bytesArray[0].GetBytes());
        }

        [TestMethod]
        public void DataMap_FullCycle_EnumMap()
        {
            NestedTypes n;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "enumMap", new Dictionary<string, object>()
                    {
                        { "one",  "SYMBOL_1" }
                    }
                }
            };

            n = new NestedTypes(data);
            
            Assert.IsTrue(n.hasEnumMap);
            Assert.AreEqual(expectedEnum, n.enumMap["one"].symbol);

            NestedTypes reclaimed = new NestedTypes(n.Data());

            Assert.IsTrue(reclaimed.hasEnumMap);
            Assert.AreEqual(expectedEnum, reclaimed.enumMap["one"].symbol);
        }

        [TestMethod]
        public void DataMap_FullCycle_EnumArray()
        {
            NestedTypes n;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "enumArray", new List<object>()
                    {
                        "SYMBOL_1"
                    }
                }
            };

            n = new NestedTypes(data);
            
            Assert.IsTrue(n.hasEnumArray);
            Assert.AreEqual(expectedEnum, n.enumArray[0].symbol);

            NestedTypes reclaimed = new NestedTypes(n.Data());

            Assert.IsTrue(reclaimed.hasEnumArray);
            Assert.AreEqual(expectedEnum, reclaimed.enumArray[0].symbol);
        }

        [TestMethod]
        public void DataMap_FullCycle_UnionMap()
        {
            NestedTypes n;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "unionMap", new Dictionary<string, object>()
                    {
                        { "one", new Dictionary<string, object>()
                            {
                                { "int", expectedInt }
                            }
                        }
                    }
                }
            };

            n = new NestedTypes(data);
            
            Assert.IsTrue(n.hasUnionMap);
            Assert.AreEqual(NestedTypes.UnionMap.Member.Int, n.unionMap["one"].member);
            Assert.AreEqual(expectedInt, n.unionMap["one"].asInt);

            NestedTypes reclaimed = new NestedTypes(n.Data());

            Assert.IsTrue(reclaimed.hasUnionMap);
            Assert.AreEqual(NestedTypes.UnionMap.Member.Int, reclaimed.unionMap["one"].member);
            Assert.AreEqual(expectedInt, reclaimed.unionMap["one"].asInt);
        }

        [TestMethod]
        public void DataMap_FullCycle_UnionArray()
        {
            NestedTypes n;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "unionArray", new List<object>()
                    {
                        new Dictionary<string, object>()
                        {
                            { "bytes", "\u0000\u0001\u0002\u0003" }
                        }
                    }
                }
            };

            n = new NestedTypes(data);

            Assert.IsTrue(n.hasUnionArray);
            Assert.AreEqual(NestedTypes.UnionArray.Member.Bytes, n.unionArray[0].member);
            CollectionAssert.AreEqual(expectedBytes, n.unionArray[0].asBytes.GetBytes());

            NestedTypes reclaimed = new NestedTypes(n.Data());

            Assert.IsTrue(reclaimed.hasUnionArray);
            Assert.AreEqual(NestedTypes.UnionArray.Member.Bytes, reclaimed.unionArray[0].member);
            CollectionAssert.AreEqual(expectedBytes, reclaimed.unionArray[0].asBytes.GetBytes());
        }
    }
}