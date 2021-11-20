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

        /// <summary>
        /// The maximum amount of times to traverse upwards when attempting to find a git directory
        /// </summary>
        private const int MAX_SEARCH_DEPTH = 5;

        private string _path = String.Empty;

        public string PathName
        {
            get { return _path; }
            set
            {
                if (!_directoryManager.Exists(value))
                    throw new ArgumentException("supplied path does not exist");

                _path = value;
            }
        }

        private IDirectoryManager _directoryManager;

        public Workspace(string pathname)
        {
            _directoryManager = new DirectoryManager();


            PathName = pathname;
        }
        public Workspace()
        {
            _directoryManager = new DirectoryManager();

            PathName = FindRootGitDirectory();
        }

        public Workspace(string pathName, IDirectoryManager directoryManager)
        {
            _directoryManager = directoryManager;


            PathName = pathName;
        }

        public List<string> ListFiles()
        {
            return Directory.GetFiles(PathName, "*.*", SearchOption.AllDirectories)
                .Where(x => !x.Substring(2).Split("\\")[1].ContainsAnyInList(IGNORE_LIST)).ToList();
        }

        public byte[] ReadFile(string path)
        {
            return _directoryManager.Read(String.Join(PathName, path));
        }

        private string FindRootGitDirectory()
        {
            var path = ".";
            var i = 0;
            while (i <= MAX_SEARCH_DEPTH)
            {
                if (_directoryManager.EnumerateDirectories(path, ".git").FirstOrDefault() == null)
                {
                    path = "..\\" + path;
                    i++;
                    continue;
                }
                else
                {
                    // Attempt to use the full path, but if not we use the relative path
                    try
                    {
                        return Path.GetFullPath(path);
                    }
                    catch (Exception)
                    {
                        return path;
                    }
                }
            }

            throw new Exception($"Unable to find a git repository, searched {MAX_SEARCH_DEPTH} upwards");
        }


    }
}
