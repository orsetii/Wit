using System.CommandLine;
using System.CommandLine.Invocation;
using Wit.Core;
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

        private Author _author;



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
        /// Explicitly set the root path for the workspace
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
            cmd.AddOption(new Option("--author-email", "The name of the author of this commit"));
            cmd.AddOption(new Option("--author-name", "The name of the author of this commit"));

            cmd.Handler = CommandHandler.Create(Handle);

            return cmd;
        }


        public void Handle(string? authorEmail, string? authorName)
        {
            _author = new Author(authorName, authorEmail);
            


            _fileList = _ws.ListFiles();
            
            var tree = new Tree();
            
            
            foreach (var path in _fileList)
            {
                var data = _ws.ReadFile(path);
                var blob = new Blob(data);

                _db.Store(blob);

                tree.Entries.Add(new Entry(path, blob.Oid));
            }
            _db.Store(tree);

        }
        

    }
}
