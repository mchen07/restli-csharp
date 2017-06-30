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
    public class NestedCollectionsTests
    {
        [TestMethod]
        public void DataMap_OmitAll()
        {
            NestedCollections n = new NestedCollections(new Dictionary<string, object>());

            Assert.IsFalse(n.hasCollectionWithDefault);
            Assert.IsFalse(n.hasNestedArray);
            Assert.IsFalse(n.hasNestedMap);
            Assert.IsFalse(n.hasMixed);
        }

        [TestMethod]
        public void DataMap_NestedArray()
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
            Assert.IsFalse(n.hasNestedMap);
            Assert.IsFalse(n.hasMixed);
            Assert.AreEqual("hello, nested!", n.nestedArray[0][0][0].stringField);
            Assert.AreEqual("another", n.nestedArray[0][1][0].stringField);
        }

        [TestMethod]
        public void DataMap_NestedMap()
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
            Assert.IsTrue(n.hasNestedMap);
            Assert.IsFalse(n.hasMixed);
            Assert.AreEqual("one, two", n.nestedMap["one"]["two"].stringField);
            Assert.AreEqual("one, extra", n.nestedMap["one"]["extra"].stringField);
        }

        [TestMethod]
        public void DataMap_Mixed()
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
            Assert.IsFalse(n.hasNestedMap);
            Assert.IsTrue(n.hasMixed);
            Assert.AreEqual("0, one, 0", n.mixed[0]["one"][0].stringField);
            Assert.AreEqual("0, one, 1", n.mixed[0]["one"][1].stringField);
            Assert.AreEqual("0, extra, 0", n.mixed[0]["extra"][0].stringField);
        }

        [TestMethod]
        public void Builder()
        {
            NestedCollections n;

            NestedCollectionsBuilder b = new NestedCollectionsBuilder();
            b.mixed = new List<Dictionary<string, List<SimpleRecord>>>()
                {
                    new Dictionary<string, List<SimpleRecord>>()
                    {
                        { "one",
                            new List<SimpleRecord>()
                            {
                                new SimpleRecord(new Dictionary<string, object>()
                                {
                                    { "stringField", "0, one, 0" }
                                }),
                                new SimpleRecord(new Dictionary<string, object>()
                                {
                                    { "stringField", "0, one, 1" }
                                })
                            }
                        },
                        { "extra",
                            new List<SimpleRecord>()
                            {
                                new SimpleRecord(new Dictionary<string, object>()
                                {
                                    { "stringField", "0, extra, 0" }
                                })
                            }
                        }
                    }
                };

            n = b.Build();

            Assert.IsFalse(n.hasCollectionWithDefault);
            Assert.IsFalse(n.hasNestedArray);
            Assert.IsFalse(n.hasNestedMap);
            Assert.IsTrue(n.hasMixed);
            Assert.AreEqual("0, one, 0", n.mixed[0]["one"][0].stringField);
            Assert.AreEqual("0, one, 1", n.mixed[0]["one"][1].stringField);
            Assert.AreEqual("0, extra, 0", n.mixed[0]["extra"][0].stringField);
        }

        [TestMethod]
        public void FullCycle()
        {
            NestedCollections n;

            NestedCollectionsBuilder b = new NestedCollectionsBuilder();
            b.mixed = new List<Dictionary<string, List<SimpleRecord>>>()
                {
                    new Dictionary<string, List<SimpleRecord>>()
                    {
                        { "one",
                            new List<SimpleRecord>()
                            {
                                new SimpleRecord(new Dictionary<string, object>()
                                {
                                    { "stringField", "0, one, 0" }
                                }),
                                new SimpleRecord(new Dictionary<string, object>()
                                {
                                    { "stringField", "0, one, 1" }
                                })
                            }
                        },
                        { "extra",
                            new List<SimpleRecord>()
                            {
                                new SimpleRecord(new Dictionary<string, object>()
                                {
                                    { "stringField", "0, extra, 0" }
                                })
                            }
                        }
                    }
                };
            b.nestedMap = new Dictionary<string, Dictionary<string, SimpleRecordProjection>>()
                {
                    { "one",
                        new Dictionary<string, SimpleRecordProjection>()
                        {
                            { "two",
                                new SimpleRecordProjection(new Dictionary<string, object>()
                                {
                                    { "stringField", "one, two" }
                                })
                            },
                            { "extra",
                                new SimpleRecordProjection(new Dictionary<string, object>()
                                {
                                    { "stringField", "one, extra" }
                                })
                            }
                        }
                    }
                };

            n = b.Build();

            NestedCollections reclaimed = new NestedCollections(n.Data());

            Assert.AreNotSame(n, reclaimed);
            Assert.IsFalse(n.hasCollectionWithDefault || reclaimed.hasCollectionWithDefault);
            Assert.IsFalse(n.hasNestedArray || reclaimed.hasNestedArray);
            Assert.IsTrue(n.hasNestedMap && reclaimed.hasNestedMap);
            Assert.IsTrue(n.hasMixed && reclaimed.hasMixed);
            Assert.AreEqual(n.nestedMap["one"]["two"].stringField, reclaimed.nestedMap["one"]["two"].stringField);
            Assert.AreEqual(n.nestedMap["one"]["extra"].stringField, reclaimed.nestedMap["one"]["extra"].stringField);
            Assert.AreEqual(n.mixed[0]["one"][0].stringField, reclaimed.mixed[0]["one"][0].stringField);
            Assert.AreEqual(n.mixed[0]["one"][1].stringField, reclaimed.mixed[0]["one"][1].stringField);
            Assert.AreEqual(n.mixed[0]["extra"][0].stringField, reclaimed.mixed[0]["extra"][0].stringField);
        }
    }
}