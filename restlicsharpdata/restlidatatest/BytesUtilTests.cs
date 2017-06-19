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

using restlicsharpdata.restlidata;

namespace restlicsharpdata.restlidataintegration
{
    [TestClass]
    public class BytesUtilTests
    {
        [TestMethod]
        public void BytesUtil_StringToBytes()
        {
            byte[] expected = new byte[] {
                0, 1, 2, 3,
                48, 49, 50, 51,
                65, 66, 67, 68,
                161, 162, 163, 164};
            string input = "\u0000\u0001\u0002\u00030123ABCD\u00a1\u00a2\u00a3\u00a4";

            byte[] actual = BytesUtil.StringToBytes(input);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BytesUtil_InvalidString()
        {
            string input = "\u0000\u0001\u00ffINVALID\u0100\u1000";

            byte[] output = BytesUtil.StringToBytes(input);
        }
    }
}