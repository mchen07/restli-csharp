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
    public class Bytes
    {
        private byte[] _bytes;

        public Bytes(byte[] bytes)
        {
            _bytes = new byte[bytes.Length];
            Array.Copy(bytes, _bytes, bytes.Length);
        }

        public byte[] GetBytes()
        {
            byte[] bytesCopy = new byte[_bytes.Length];
            Array.Copy(_bytes, bytesCopy, _bytes.Length);
            return bytesCopy;
        }
    }
}
