namespace Wit.Services
{
    public class DirectoryManager : IDirectoryManager
    {
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public DirectoryInfo Create(string path)
        {
            return Directory.CreateDirectory(Path.GetFullPath(path));
        }

        public void Move(string sourceDirName, string destDirName)
        {
            Directory.Move(sourceDirName, destDirName);
        }

        public IEnumerable<DirectoryInfo> EnumerateDirectories(string path, string searchPattern)
        {
            var dir = new DirectoryInfo(path);
            return dir.EnumerateDirectories(searchPattern);
        }

        public byte[] Read(string path)
        {
            return File.ReadAllBytes(path);
        }

    }
}
