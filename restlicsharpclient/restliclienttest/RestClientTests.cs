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
using System.IO;

using com.linkedin.restli.test.api;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclienttest
{
    [TestClass]
    public class RestClientTests
    {
        [TestMethod]
        public void DeserializeDataMap()
        {
            string dataMapString = @"
                {
                    'foo': 'bar',
                    'nested': {
                        'foofoo': 'barbar',
                        'list': [1, 2, 3]
                    },
                    'list': [10, 20, 30],
                    'null': null,
                    'primitive': 42,
                    'one': {
                        'two': {
                            'three': {
                                'four': [1, 'hello', null, 4.2]
                            }
                        }
                    },
                    'complexlist': [1, [2, [3, 4]]]
                }";

            Dictionary<string, object> dataMap = DataUtil.StringToMap(dataMapString);

            Assert.AreEqual("bar", dataMap["foo"]);

            Dictionary<string, object> nested = (Dictionary<string, object>)dataMap["nested"];
            Assert.AreEqual("barbar", nested["foofoo"]);
            CollectionAssert.AreEqual(new List<object> { (long)1, (long)2, (long)3 }, (List<object>)nested["list"], "Failed on [1, 2, 3]");

            CollectionAssert.AreEqual(new List<object> { (long)10, (long)20, (long)30 }, (List<object>)dataMap["list"], "Failed on [10, 20, 30]");
            Assert.IsNull(dataMap["null"]);
            Assert.AreEqual((long)42, dataMap["primitive"]);

            CollectionAssert.AreEqual(new List<object> { (long)1, "hello", null, (double)4.2 }, (List<object>)(((dataMap["one"] as Dictionary<string, object>)["two"] as Dictionary<string, object>)["three"] as Dictionary<string, object>)["four"], "Failed on [1, 'hello', null, 4.2]");

            Assert.AreEqual((long)4, (((dataMap["complexlist"] as List<object>)[1] as List<object>)[1] as List<object>)[1]);
        }

        [TestMethod]
        public void StreamReadAllBytes()
        {
            byte[] original = { 48, 49, 50, 51, 0, 1, 2, 3, 4};
            Stream stream = new MemoryStream(original);
            byte[] reclaimed = stream.ReadAllBytes();

            CollectionAssert.AreEqual(original, reclaimed);            
        }

        [TestMethod]
        public void SerializeAndDeserializeRecordWithEnum()
        {
            GreetingBuilder greetingBuilder = new GreetingBuilder();
            greetingBuilder.id = 123;
            greetingBuilder.tone = new Tone(Tone.Symbol.SINCERE);
            greetingBuilder.message = "Hello, Serialize test!";
            Greeting g = greetingBuilder.Build();

            string serialized = DataUtil.MapToString(g.Data());

            Dictionary<string, object> dataMap = DataUtil.StringToMap(serialized);
            Greeting reclaimed = new Greeting(dataMap);

            Assert.AreEqual(g.id, reclaimed.id);
            Assert.AreEqual(g.message, reclaimed.message);
            Assert.AreEqual(g.tone.symbol, reclaimed.tone.symbol);
        }
        
        [TestMethod]
        public void MiscMethod()
        {
            string dataString = @"{'exceptionClass': 'com.linkedin.restli.server.RestLiServiceException','message': 'Negative key.','stackTrace': 'com.linkedin.restli.server.RestLiServiceException[HTTP Status: 400]: Negative key.
                                    at com.linkedin.restli.test.resources.BasicCollectionResource.get(BasicCollectionResource.java:72)

                at sun.reflect.GeneratedMethodAccessor1.invoke(Unknown Source)

                at sun.reflect.DelegatingMethodAccessorImpl.invoke(DelegatingMethodAccessorImpl.java:43)

                at java.lang.reflect.Method.invoke(Method.java:497)

                at com.linkedin.restli.internal.server.RestLiMethodInvoker.doInvoke(RestLiMethodInvoker.java:230)
	            at com.linkedin.restli.internal.server.RestLiMethodInvoker.access$100(RestLiMethodInvoker.java:60)
	            at com.linkedin.restli.internal.server.RestLiMethodInvoker$RestLiRequestFilterChainCallbackImpl.onSuccess(RestLiMethodInvoker.java:372)
	            at com.linkedin.restli.internal.server.filter.RestLiRequestFilterChain.onRequest(RestLiRequestFilterChain.java:72)
	            at com.linkedin.restli.internal.server.RestLiMethodInvoker.invoke(RestLiMethodInvoker.java:189)
	            at com.linkedin.restli.server.RestLiServer.handleResourceRequest(RestLiServer.java:303)

                at com.linkedin.restli.server.RestLiServer.doHandleRequest(RestLiServer.java:196)

                at com.linkedin.restli.server.BaseRestServer.handleRequest(BaseRestServer.java:56)

                at com.linkedin.restli.server.DelegatingTransportDispatcher.handleRestRequest(DelegatingTransportDispatcher.java:61)

                at com.linkedin.r2.filter.transport.DispatcherRequestFilter.onRestRequest(DispatcherRequestFilter.java:68)

                at com.linkedin.r2.filter.FilterChainIterator$FilterChainRestIterator.doOnRequest(FilterChainIterator.java:131)

                at com.linkedin.r2.filter.FilterChainIterator$FilterChainRestIterator.doOnRequest(FilterChainIterator.java:117)

                at com.linkedin.r2.filter.FilterChainIterator.onRequest(FilterChainIterator.java:57)

                at com.linkedin.r2.filter.transport.ServerQueryTunnelFilter.onRestRequest(ServerQueryTunnelFilter.java:58)

                at com.linkedin.r2.filter.FilterChainIterator$FilterChainRestIterator.doOnRequest(FilterChainIterator.java:131)

                at com.linkedin.r2.filter.FilterChainIterator$FilterChainRestIterator.doOnRequest(FilterChainIterator.java:117)

                at com.linkedin.r2.filter.FilterChainIterator.onRequest(FilterChainIterator.java:57)

                at com.linkedin.r2.filter.message.rest.RestFilter.onRestRequest(RestFilter.java:50)

                at com.linkedin.r2.filter.FilterChainIterator$FilterChainRestIterator.doOnRequest(FilterChainIterator.java:131)

                at com.linkedin.r2.filter.FilterChainIterator$FilterChainRestIterator.doOnRequest(FilterChainIterator.java:117)

                at com.linkedin.r2.filter.FilterChainIterator.onRequest(FilterChainIterator.java:57)

                at com.linkedin.r2.filter.FilterChainImpl.onRestRequest(FilterChainImpl.java:86)

                at com.linkedin.r2.filter.transport.FilterChainDispatcher.handleRestRequest(FilterChainDispatcher.java:70)

                at com.linkedin.r2.transport.http.server.HttpDispatcher.handleRequest(HttpDispatcher.java:95)

                at com.linkedin.r2.transport.http.server.HttpDispatcher.handleRequest(HttpDispatcher.java:70)

                at com.linkedin.r2.transport.http.server.HttpNettyServer$RestHandler.channelRead0(HttpNettyServer.java:173)

                at com.linkedin.r2.transport.http.server.HttpNettyServer$RestHandler.channelRead0(HttpNettyServer.java:136)

                at io.netty.channel.SimpleChannelInboundHandler.channelRead(SimpleChannelInboundHandler.java:105)

                at io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:339)

                at io.netty.channel.AbstractChannelHandlerContext.access$600(AbstractChannelHandlerContext.java:32)

                at io.netty.channel.AbstractChannelHandlerContext$7.run(AbstractChannelHandlerContext.java:329)

                at io.netty.util.concurrent.DefaultEventExecutor.run(DefaultEventExecutor.java:36)

                at io.netty.util.concurrent.SingleThreadEventExecutor$2.run(SingleThreadEventExecutor.java:111)

                at io.netty.util.concurrent.DefaultThreadFactory$DefaultRunnableDecorator.run(DefaultThreadFactory.java:137)

                at java.lang.Thread.run(Thread.java:745)
            ','status': 400}";

            Dictionary<string, object> dataMap = DataUtil.StringToMap(dataString);

            int i = 1;
        }
    }
}