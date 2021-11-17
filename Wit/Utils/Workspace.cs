using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wit.Services;
using Wit.Extensions;

namespace Wit.Utils
{
    public class Workspace
    {
        // note that we would need to ignore the '.' and '..' directories on linux
        // and change the parsing behaviour to ignore differently as each path on linux uses '.' extensively.
        private readonly List<string> IGNORE_LIST = new List<string> { ".git" };

        private string _pathName;

        private IDirectoryManager _directoryManager;

        public Workspace(string pathname)
        {
            _directoryManager = new DirectoryManager();
            if (!_directoryManager.Exists(pathname))
                throw new ArgumentException("Supplied path does not exist");


            _pathName = pathname;
        }

        public Workspace(string pathName, IDirectoryManager directoryManager)
        {
            _directoryManager = directoryManager;

            if (!_directoryManager.Exists(pathName))
                throw new ArgumentException("Supplied path does not exist");

            _pathName = pathName;
        }

        public List<string> ListFiles()
        {
            return Directory.GetFiles(_pathName, "*.*", SearchOption.AllDirectories)
                .Where(x => !x.Substring(2).Split("\\")[1].ContainsAnyInList(IGNORE_LIST)).ToList();
        }

        public byte[] ReadFile(string path)
        {
            return _directoryManager.Read(String.Join(_pathName, path));
        }
    }
}
