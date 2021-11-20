using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wit.Services;

namespace Wit.Data
{
    public class Blob : IGitObject
    {
        private byte[] _data;

        public GitObjectType GitType => GitObjectType.Blob;


        public byte[] Data => _data;

        public Blob(string data)
        {
            _data = Encoding.UTF8.GetBytes(data);
        }

        public Blob(byte[] data)
        {
            _data = data;
        }

        public override string ToString()
        {
            return _data.ToString() ?? string.Empty;
        }
    }
}
