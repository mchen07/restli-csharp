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

            Assert.AreEqual(TestEnum.Value.SYMBOL_1, e.value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEnum_Invalid()
        {
            TestEnum e = new TestEnum("foobar");
        }
    }
}