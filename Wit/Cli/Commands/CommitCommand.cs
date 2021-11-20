using System.CommandLine;
using System.CommandLine.Invocation;
using Wit.Utils;
using Wit.Data;

namespace Wit.Cli.Commands
{
    public class CommitCommand : ICliCommand
    {

        private readonly string _rootPath;

        private string _gitPath => Path.Combine(_rootPath, ".git");

        private string _dbPath => Path.Combine(_gitPath, "objects");

        private List<string> _fileList = new();

        private Workspace _ws;

        private Database _db;


        /// <summary>
        /// Finds the root git directory by traversing upwards from the current directory
        /// </summary>
        public CommitCommand()
        {
            _ws = new Workspace();

            _rootPath = _ws.PathName;
            _db = new Database(_dbPath);
        }

        /// <summary>
        /// Explictly set the root path for the workspace
        /// </summary>
        public CommitCommand(string rootPath)
        {
            _rootPath = rootPath;

            _ws = new Workspace(_rootPath);
            _db = new Database(_dbPath);
        }

        public Command CreateCommand()
        {
            var cmd = new Command("commit", "Commit changes to the repository.");

            cmd.Handler = CommandHandler.Create(this.Handle);

            return cmd;
        }


        public void Handle()
        {
            _fileList = _ws.ListFiles();

            foreach (var path in _fileList)
            {
                var data = _ws.ReadFile(path);
                var blob = new Blob(data);

                _db.Store(blob);
            }


        }

    }
}
