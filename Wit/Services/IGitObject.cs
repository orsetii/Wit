using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Wit.Services
{
    public interface IGitObject
    {
        public GitObjectType GitType { get; }
        public byte[] Data { get; }
    }

    public class GitObjectType
    {
        private GitObjectType(string value) { Value = value; }

        public string Value { get; }

        public static GitObjectType Blob => new("blob");

        public override string ToString()
        {
            return Value;
        }
    }

}
