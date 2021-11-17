using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Wit.App.Cli.Commands;

namespace Wit.App;
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
    }
}