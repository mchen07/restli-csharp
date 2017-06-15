using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using com.linkedin.restli.datagenerator.integration;

namespace restlicsharpdata.restlidataintegration
{
    [TestClass]
    public class TestEnumTests
    {
        [TestMethod]
        public void TestEnum_Basic()
        {
            TestEnum e = new TestEnum("SYMBOL_1");

            Assert.AreEqual(TestEnum.Symbol.SYMBOL_1, e.symbol);
        }

        [TestMethod]
        public void TestEnum_Invalid()
        {
            TestEnum e = new TestEnum("foobar");

            Assert.AreEqual(TestEnum.Symbol.UNKNOWN, e.symbol);
        }
    }
}