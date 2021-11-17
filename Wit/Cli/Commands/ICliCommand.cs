using System.CommandLine;

namespace Wit.Cli.Commands
{
    public interface ICliCommand
    {
        Command CreateCommand();
    }
}