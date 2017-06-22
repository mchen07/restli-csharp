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

using com.linkedin.restli.test.api;

namespace restlicsharpclient.restliclientintegrationtest
{
    [TestClass]
    public class DataTemplateTests
    {
        [TestMethod]
        public void DataTemplate_BuildGreeting()
        {
            GreetingBuilder b = new GreetingBuilder();
            b.id = 456;
            b.message = "This is the Greeting Builder test!";
            b.tone = new Tone(Tone.Symbol.FRIENDLY);
            Greeting g = b.Build();

            Assert.IsTrue(g.hasId);
            Assert.IsTrue(g.hasMessage);
            Assert.IsTrue(g.hasTone);
            Assert.AreEqual(456, g.id);
            Assert.AreEqual("This is the Greeting Builder test!", g.message);
            Assert.AreEqual(Tone.Symbol.FRIENDLY, g.tone.symbol);
        }
    }
}