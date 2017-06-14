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
