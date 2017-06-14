using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

using com.linkedin.restli.datagenerator.integration;

namespace restlicsharpdata.restlidataintegration
{
    [TestClass]
    public class SimpleRecordTests
    {
        [TestMethod]
        public void SimpleRecord_DataMapInit()
        {
            SimpleRecord s;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "stringField", "hello" },
                { "intValue", 5 },
                { "anotherIntValue", 6 }
            };           

            s = new SimpleRecord(data);

            Assert.AreEqual("hello", s.stringField);
            Assert.AreEqual(5, s.intValue);
            Assert.AreEqual(6, s.anotherIntValue);
            Assert.IsTrue(s.hasIntValue);
            Assert.IsTrue(s.hasAnotherIntValue);
        }

        [TestMethod]
        public void SimpleRecord_DataMapInit_Defaults()
        {
            SimpleRecord s;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "stringField", "foobar" }
            };

            s = new SimpleRecord(data);

            Assert.AreEqual("foobar", s.stringField);
            Assert.AreEqual(1, s.intValue);
            Assert.AreEqual(2, s.anotherIntValue);
            Assert.IsFalse(s.hasIntValue);
            Assert.IsFalse(s.hasAnotherIntValue);
        }

        [TestMethod]
        public void SimpleRecord_Builder()
        {
            SimpleRecord s;

            SimpleRecordBuilder b = new SimpleRecordBuilder();
            b.stringField = "hello";
            b.intValue = 5;
            b.anotherIntValue = 6;

            s = b.Build();

            Assert.AreEqual("hello", s.stringField);
            Assert.AreEqual(5, s.intValue);
            Assert.AreEqual(6, s.anotherIntValue);
            Assert.IsTrue(s.hasIntValue);
            Assert.IsTrue(s.hasAnotherIntValue);
        }

        [TestMethod]
        public void SimpleRecord_Builder_Defaults()
        {
            SimpleRecord s;

            SimpleRecordBuilder b = new SimpleRecordBuilder();
            b.stringField = "hello";

            s = b.Build();

            Assert.AreEqual("hello", s.stringField);
            Assert.AreEqual(1, s.intValue);
            Assert.AreEqual(2, s.anotherIntValue);
            Assert.IsFalse(s.hasIntValue);
            Assert.IsFalse(s.hasAnotherIntValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SimpleRecord_Builder_OmitRequired()
        {
            SimpleRecord s;

            SimpleRecordBuilder b = new SimpleRecordBuilder();
            b.intValue = 5;
            b.anotherIntValue = 6;

            s = b.Build();
        }
    }
}