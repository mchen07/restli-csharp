using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

using restlicsharpdata.restlidata;

namespace restlicsharpdata.restlidataintegration
{
    [TestClass]
    public class BytesTests
    {
        [TestMethod]
        public void Bytes_Init()
        {
            byte[] expected = new byte[] { 0, 1, 2, 3 };
            string input = "\u0000\u0001\u0002\u0003";

            try
            {
                Bytes output = new Bytes(BytesUtil.StringToBytes(input));
            }
            catch
            {
                Assert.Fail("Exception while instantiating Bytes object.");
            }
        }

        [TestMethod]
        public void Bytes_Immutable()
        {
            string input = "\u0000\u0001\u0002\u0003";

            Bytes output = new Bytes(BytesUtil.StringToBytes(input));

            byte[] copy = output.GetBytes();
            const byte VALUE = 0x00ff;
            copy[0] = VALUE;
            CollectionAssert.AreNotEqual(copy, output.GetBytes());

            byte[] copy2 = output.GetBytes();
            const byte VALUE2 = 0x00ee;
            copy2[0] = VALUE2;
            CollectionAssert.AreNotEqual(copy, output.GetBytes());

            // Test that both copies are separate copies
            Assert.AreNotSame(copy2, copy);
            CollectionAssert.AreNotEqual(copy2, copy);
        }

        [TestMethod]
        public void Bytes_CopyInitArg()
        {
            string input = "\u0000\u0001\u0002\u0003";
            byte[] inputBytes = BytesUtil.StringToBytes(input);

            Bytes output = new Bytes(inputBytes);

            Assert.AreNotSame(inputBytes, output.GetBytes());

            byte VALUE = 0x00ff;
            inputBytes[0] = VALUE;
            Assert.AreNotEqual(VALUE, output.GetBytes()[0]);
        }
    }
}