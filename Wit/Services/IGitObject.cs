using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wit.Services
{
    public interface IGitObject
    {
        public string GitType { get; }
        public byte[] Data { get; }
    }
}
