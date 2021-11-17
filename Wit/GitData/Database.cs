using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Wit.Services;

namespace Wit.GitData
{
    public class Database
    {

        private string _path;
        public Database(string path)
        {
            _path = path;
        }

        public void Store(IGitObject gitObject)
        {
            var content = Encoding.UTF8.GetBytes($"{gitObject.GitType} {gitObject.Data.Length}\0{gitObject.Data}");

            var oid = SHA1.Create().ComputeHash(content);
            WriteObject(oid, content);
        }

        private void WriteObject(byte[] oid, byte[] content)
        {
            // TODO
        }
    }
}
