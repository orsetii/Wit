using System.CommandLine;

namespace Wit.App.Cli.Commands
{
    public interface ICliCommand
    {
        Command CreateCommand();
    }
}