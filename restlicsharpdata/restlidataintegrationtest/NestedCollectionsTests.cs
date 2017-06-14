using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

using com.linkedin.restli.datagenerator.integration;

namespace restlicsharpdata.restlidataintegration
{
    [TestClass]
    public class NestedCollectionsTests
    {
        [TestMethod]
        public void NestedCollections_DataMap_OmitAll()
        {
            NestedCollections n = new NestedCollections(new Dictionary<string, object>());

            Assert.IsFalse(n.hasCollectionWithDefault);
            Assert.IsFalse(n.hasNestedArray);
        }

        [TestMethod]
        public void NestedCollections_DataMap_NestedArray()
        {
            NestedCollections n;

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("nestedArray", new List<List<List<Dictionary<string, object>>>>()
                {
                    new List<List<Dictionary<string, object>>>()
                    {
                        new List<Dictionary<string, object>>()
                        {
                            new Dictionary<string, object>()
                            {
                                { "stringField", "hello, nested!" }
                            }
                        },
                        new List<Dictionary<string, object>>()
                        {
                            new Dictionary<string, object>()
                            {
                                { "stringField", "another" }
                            }
                        }
                    }
                });

            n = new NestedCollections(data);

            Assert.IsFalse(n.hasCollectionWithDefault);
            Assert.IsTrue(n.hasNestedArray);
            Assert.AreEqual("hello, nested!", n.nestedArray[0][0][0].stringField);
            Assert.AreEqual("another", n.nestedArray[0][1][0].stringField);
        }

        [TestMethod]
        public void NestedCollections_DataMap_NestedMap()
        {
            NestedCollections n;

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("nestedMap", new Dictionary<string, Dictionary<string, Dictionary<string, object>>>()
                {
                    { "one",
                        new Dictionary<string, Dictionary<string, object>>()
                        {
                            { "two",
                                new Dictionary<string, object>()
                                {
                                    { "stringField", "one, two" }
                                }
                            },
                            { "extra",
                                new Dictionary<string, object>()
                                {
                                    { "stringField", "one, extra" }
                                }
                            }
                        }
                    }
                });

            n = new NestedCollections(data);

            Assert.IsFalse(n.hasCollectionWithDefault);
            Assert.IsFalse(n.hasNestedArray);
            Assert.AreEqual("one, two", n.nestedMap["one"]["two"].stringField);
            Assert.AreEqual("one, extra", n.nestedMap["one"]["extra"].stringField);
        }

        [TestMethod]
        public void NestedCollections_DataMap_Mixed()
        {
            NestedCollections n;

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("mixed", new List<Dictionary<string, List<Dictionary<string, object>>>>()
                {
                    new Dictionary<string, List<Dictionary<string, object>>>()
                    {
                        { "one",
                            new List<Dictionary<string, object>>()
                            {
                                new Dictionary<string, object>()
                                {
                                    { "stringField", "0, one, 0" }
                                },
                                new Dictionary<string, object>()
                                {
                                    { "stringField", "0, one, 1" }
                                }
                            }
                        },
                        { "extra",
                            new List<Dictionary<string, object>>()
                            {
                                new Dictionary<string, object>()
                                {
                                    { "stringField", "0, extra, 0" }
                                }
                            }
                        }
                    }
                });

            n = new NestedCollections(data);

            Assert.IsFalse(n.hasCollectionWithDefault);
            Assert.IsFalse(n.hasNestedArray);
            Assert.AreEqual("0, one, 0", n.mixed[0]["one"][0].stringField);
            Assert.AreEqual("0, one, 1", n.mixed[0]["one"][1].stringField);
            Assert.AreEqual("0, extra, 0", n.mixed[0]["extra"][0].stringField);
        }
    }
}