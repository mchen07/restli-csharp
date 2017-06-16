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
