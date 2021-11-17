using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wit.Services;

namespace Wit.Utils
{
    public class Workspace
    {
        private readonly string[] IGNORE_LIST = new string[] { ".", "..", ".git" };

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

        public string[] ListFiles()
        {
            return Directory.GetFiles(_pathName);
        }
    }
}
