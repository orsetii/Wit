using System.Security.Cryptography;
using System.Text;
using Wit.Services;
using ZstdNet;

namespace Wit.Data
{
    public class Database
    {

        private string _path;
        private IDirectoryManager _directoryManager;
        public Database(string path)
        {
            _path = path;
            _directoryManager = new DirectoryManager();
        }
        public Database(string path, IDirectoryManager directoryManager)
        {
            _path = path;
            _directoryManager = directoryManager;
        }

        public void Store(IGitObject gitObject)
        {
            var content = Encoding.Default.GetBytes($"{gitObject.GitType} {gitObject.Data.Length}\0")
                                                        .Concat(gitObject.Data).ToArray();

            var oid = SHA1.Create().ComputeHash(content);
            WriteObject(oid, content);
        }

        private void WriteObject(byte[] oid, byte[] content)
        {
            // Create our variables to determine where this object will be stored on disk
            // both permanently and temporarily while we are compressing it.
            var objectPath = Path.Combine(
                            _path,
                            oid[0].ToString("X2"),
                            String.Concat(content.Select(x => x.ToString("X2"))));
            var dirName = Path.GetDirectoryName(objectPath) ??
                                                throw new InvalidOperationException("Unable to get directory name for objectPath");

            var tempPath = Path.GetTempFileName();

            if (!_directoryManager.Exists(dirName))
                _directoryManager.Create(dirName);

            CompressAndWriteFile(tempPath, content);

            // Finally, move the file to its permanent location
            File.Move(tempPath, objectPath);
        }

        private void CompressAndWriteFile(string path, byte[] data)
        {
            var objectFile = File.Open(path, FileMode.OpenOrCreate);

            // Compress this data using zstd
            var compressor = new Compressor();
            var compressedData = compressor.Wrap(data) ?? throw new InvalidOperationException("Unable to compress data");
            objectFile.Write(compressedData);
            objectFile.Close();
        }

        public static byte[] ReadCompressedFile(string path)
        {
            var dc = new Decompressor();
            return dc.Unwrap(File.ReadAllBytes(path));
        }
    }
}
