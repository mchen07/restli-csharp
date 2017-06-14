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

            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> dataUnionEmpty = new Dictionary<string, object>();
            Dictionary<string, object> dataUnionWithoutNull = new Dictionary<string, object>();
            dataUnionWithoutNull.Add("int", 20);
            Dictionary<string, object> dataUnionWithInline = new Dictionary<string, object>();
            dataUnionWithInline.Add("string", "hello, world!");
            data.Add("unionEmpty", dataUnionEmpty);
            data.Add("unionWithoutNull", dataUnionWithoutNull);
            data.Add("unionWithInline", dataUnionWithInline);

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

            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> dataUnionEmpty = new Dictionary<string, object>();
            Dictionary<string, object> dataUnionWithoutNull = new Dictionary<string, object>();
            dataUnionWithoutNull.Add("bytes", "\u0000\u0001\u0002\u0003");
            Dictionary<string, object> dataUnionWithInline = new Dictionary<string, object>();
            Dictionary<string, long> stringToLong = new Dictionary<string, long>();
            stringToLong.Add("key", 9999);
            dataUnionWithInline.Add("map", stringToLong);
            data.Add("unionEmpty", dataUnionEmpty);
            data.Add("unionWithoutNull", dataUnionWithoutNull);
            data.Add("unionWithInline", dataUnionWithInline);

            u = new UnionTest(data);

            Assert.IsInstanceOfType(u.unionEmpty, typeof(UnionTest.UnionEmpty));
            Assert.IsInstanceOfType(u.unionWithoutNull, typeof(UnionTest.UnionWithoutNull));
            Assert.IsInstanceOfType(u.unionWithInline, typeof(UnionTest.UnionWithInline));

            byte[] expectedBytes = new byte[] { 0, 1, 2, 3 };
            Assert.AreEqual(UnionTest.UnionWithoutNull.Mode.Bytes, u.unionWithoutNull.dataMode);
            CollectionAssert.AreEqual(expectedBytes, u.unionWithoutNull.asBytes.GetBytes());

            Assert.AreEqual(UnionTest.UnionWithInline.Mode.Map, u.unionWithInline.dataMode);
            long actualLong;
            u.unionWithInline.asMap.TryGetValue("key", out actualLong);
            Assert.AreEqual(9999, actualLong);
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