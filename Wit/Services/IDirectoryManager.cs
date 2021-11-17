using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wit.Services
{
    public interface IDirectoryManager
    {
        bool Exists(string path);
        DirectoryInfo Create(string path);
        void Move(String sourceDirName, String destDirName);
        IEnumerable<DirectoryInfo> EnumerateDirectories(string path, string searchPattern);

        byte[] Read(string path);
    }
}
