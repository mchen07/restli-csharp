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