using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wit.App.Cli.Commands;

namespace Wit.Cli.Commands
{
    public class CommitCommand : ICliCommand
    {

        public Command CreateCommand()
        {
            var cmd = new Command("commit", "Commit changes to the repository.");


            return cmd;
        }

    }
}
