using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

using com.linkedin.restli.datagenerator.integration;

namespace restlicsharpdata.restlidataintegration
{
    [TestClass]
    public class UnionTestTests
    {
        [TestMethod]
        public void UnionTest_DataMap_IntAndString()
        {
            UnionTest u;
            
            Dictionary<string, object> dataUnionEmpty = new Dictionary<string, object>();
            Dictionary<string, object> dataUnionWithoutNull = new Dictionary<string, object>()
            {
                { "int", 20 }
            };
            Dictionary<string, object> dataUnionWithInline = new Dictionary<string, object>()
            {
                { "string", "hello, world!" }
            };
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "unionEmpty", dataUnionEmpty },
                { "unionWithoutNull", dataUnionWithoutNull },
                { "unionWithInline", dataUnionWithInline }
            };

            u = new UnionTest(data);

            Assert.IsInstanceOfType(u.unionEmpty, typeof(UnionTest.UnionEmpty));
            Assert.IsInstanceOfType(u.unionWithoutNull, typeof(UnionTest.UnionWithoutNull));
            Assert.IsInstanceOfType(u.unionWithInline, typeof(UnionTest.UnionWithInline));

            Assert.AreEqual(UnionTest.UnionWithoutNull.Mode.Int, u.unionWithoutNull.dataMode);
            Assert.AreEqual(20, u.unionWithoutNull.asInt);

            Assert.AreEqual(UnionTest.UnionWithInline.Mode.String, u.unionWithInline.dataMode);
            Assert.AreEqual("hello, world!", u.unionWithInline.asString);
        }

        [TestMethod]
        public void UnionTest_DataMap_BytesAndMap()
        {
            UnionTest u;
            
            Dictionary<string, object> dataUnionEmpty = new Dictionary<string, object>();
            Dictionary<string, object> dataUnionWithoutNull = new Dictionary<string, object>()
            {
                { "bytes", "\u0000\u0001\u0002\u0003" }
            };
            Dictionary<string, object> dataUnionWithInline = new Dictionary<string, object>()
            {
                { "map", new Dictionary<string, long>()
                    {
                    { "key", 9999 }
                    }
                }
            };
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "unionEmpty", dataUnionEmpty },
                { "unionWithoutNull", dataUnionWithoutNull },
                { "unionWithInline", dataUnionWithInline }
            };
            
            u = new UnionTest(data);

            Assert.IsInstanceOfType(u.unionEmpty, typeof(UnionTest.UnionEmpty));
            Assert.IsInstanceOfType(u.unionWithoutNull, typeof(UnionTest.UnionWithoutNull));
            Assert.IsInstanceOfType(u.unionWithInline, typeof(UnionTest.UnionWithInline));

            byte[] expectedBytes = new byte[] { 0, 1, 2, 3 };
            Assert.AreEqual(UnionTest.UnionWithoutNull.Mode.Bytes, u.unionWithoutNull.dataMode);
            CollectionAssert.AreEqual(expectedBytes, u.unionWithoutNull.asBytes.GetBytes());

            Assert.AreEqual(UnionTest.UnionWithInline.Mode.Map, u.unionWithInline.dataMode);
            Assert.AreEqual(9999, u.unionWithInline.asMap["key"]);
        }

        [TestMethod]
        public void UnionTest_Builder()
        {
            UnionTest u;

            UnionTestBuilder b = new UnionTestBuilder();
            b.unionEmpty = new UnionTest.UnionEmpty(new Dictionary<string, object>());
            b.unionWithoutNull = new UnionTest.UnionWithoutNull(new Dictionary<string, object>()
            {
                { "float", 1.23F }
            });
            b.unionWithInline = new UnionTest.UnionWithInline(new Dictionary<string, object>()
            {
                { "com.linkedin.restli.datagenerator.integration.EnumInUnionTest", "B" }
            });

            u = b.Build();

            Assert.IsInstanceOfType(u.unionEmpty, typeof(UnionTest.UnionEmpty));
            Assert.IsInstanceOfType(u.unionWithoutNull, typeof(UnionTest.UnionWithoutNull));
            Assert.IsInstanceOfType(u.unionWithInline, typeof(UnionTest.UnionWithInline));

            Assert.AreEqual(UnionTest.UnionWithoutNull.Mode.Float, u.unionWithoutNull.dataMode);
            Assert.AreEqual(1.23F, u.unionWithoutNull.asFloat);

            Assert.AreEqual(UnionTest.UnionWithInline.Mode.EnumInUnionTest, u.unionWithInline.dataMode);
            Assert.AreEqual(EnumInUnionTest.Value.B, u.unionWithInline.asEnumInUnionTest.value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UnionTest_Builder_OmitRequired()
        {
            UnionTest u;

            UnionTestBuilder b = new UnionTestBuilder();
            b.unionEmpty = new UnionTest.UnionEmpty(new Dictionary<string, object>());

            u = b.Build();
        }
    }
}