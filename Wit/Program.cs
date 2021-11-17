using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Wit.Cli.Commands;

namespace Wit;
public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var cmd = new RootCommand();
        AddCommands(cmd);


        return await cmd.InvokeAsync(args);
    }


    public static void AddCommands(RootCommand cmd)
    {

        cmd.AddCommand(new InitCommand().CreateCommand());
        cmd.AddCommand(new CommitCommand().CreateCommand());
    }
}