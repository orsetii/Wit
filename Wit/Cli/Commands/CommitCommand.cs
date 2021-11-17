using System.CommandLine;
using System.CommandLine.Invocation;
using Wit.Utils;

namespace Wit.Cli.Commands
{
    public class CommitCommand : ICliCommand
    {

        public Command CreateCommand()
        {
            var cmd = new Command("commit", "Commit changes to the repository.");

            cmd.Handler = CommandHandler.Create(Handle);

            return cmd;
        }


        public void Handle()
        {
            var ws = new Workspace("..");
            ws.ListFiles();
        }

    }
}
