using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restlicsharpdata.restlidata
{
    public class BytesUtil
    {
        /// <summary>
        /// Get bytes from string following Pegasus JSON encoding.
        /// 
        /// This method extracts the least significant 8-bits of each character in the string
        /// (following Avro convention.) The returned byte array is the same length as the string,
        /// i.e. if there are 8 characters in the string, the byte array will have 8 bytes.
        /// </summary>
        /// <param name="input">string to get bytes from</param>
        /// <returns>extracted bytes if the string is valid or validation is not enabled, else return null</returns>
        public static byte[] StringToBytes(string input)
        {
            char orChar = '\u0000';
            int length = input.Length;
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; ++i)
            {
                char c = input[i];
                orChar |= c;
                bytes[i] = (byte)(c & 0x00ff);
            }
            if ((orChar & 0xff00) != 0)
            {
                throw new ArgumentException("'" + input + "' is not a valid string representation of bytes.");
            }
            return bytes;
        }
    }
}
