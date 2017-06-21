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
using System;

using com.linkedin.restli.datagenerator.integration;

namespace restlicsharpdata.restlidataintegrationtest
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
            Assert.IsTrue(s.hasStringField);
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
            Assert.IsTrue(s.hasStringField);
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
            Assert.IsTrue(s.hasStringField);
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
            Assert.IsTrue(s.hasStringField);
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