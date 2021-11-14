using System.CommandLine.Invocation;
using System;
using System.CommandLine;
namespace Wit.App.Cli.Commands;



public class InitCommand : ICliCommand
{

    public void Handle(string dir)
    {
        // The `dir` passed in here will be either the path specified in cli or the CWD as is the default value.
        Console.WriteLine($"{dir}");
    }

    public Command CliCommand()
    {
        var cmd = new Command("init", "Initialize a new repository.");

        cmd.Add(new Option("--dir", "Intialize a new repository", typeof(string), () => Directory.GetCurrentDirectory()));

        cmd.Handler = CommandHandler.Create(Handle);

        return cmd;
    }

}