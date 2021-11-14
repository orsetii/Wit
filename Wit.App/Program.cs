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
        cmd.AddCommand(new InitCommand().CliCommand());

        return await cmd.InvokeAsync(args);
    }


    private static async Task<int> HandleEchoTimesAsync(string words, int count, int delay, bool verbose, IConsole console, CancellationToken cancellationToken)
    {
        if (count <= 0)
        {
            console.Error.WriteLine($"The count needs to be at least 1.");
            return 1;
        }

        if (delay < 0)
        {
            console.Error.WriteLine($"The delay needs to be 0 or a positive number.");
            return 1;
        }

        if (verbose)
            console.Out.WriteLine($"About to repeat '{words}' {count} time[s]...");

        for (var i = 0; i < count; i++)
        {
            console.Out.WriteLine(words);

            if (verbose)
                console.Out.WriteLine($"Sleeping for {delay}ms...");

            await Task.Delay(delay, cancellationToken);
        }

        if (verbose)
            console.Out.WriteLine($"All done!");

        return 0;
    }

    private static async Task<int> HandleEchoForeverAsync(string words, int delay, bool verbose, IConsole console, CancellationToken cancellationToken)
    {
        if (delay < 0)
        {
            console.Error.WriteLine($"The delay needs to be 0 or a positive number.");
            return 1;
        }

        if (verbose)
            console.Out.WriteLine($"About to repeat '{words}' forever...");

        while (!cancellationToken.IsCancellationRequested)
        {
            console.Out.WriteLine(words);

            if (verbose)
                console.Out.WriteLine($"Sleeping for {delay}ms...");

            await Task.Delay(delay, cancellationToken);
        }

        if (verbose)
            console.Out.WriteLine($"All done! Weird that it got here at all...");

        return 0;
    }

    private static void HandleGreeting(string name, string? greeting, bool verbose, IConsole console)
    {
        if (verbose)
            console.Out.WriteLine($"About to say hi to '{name}'...");

        greeting ??= "Hi";
        console.Out.WriteLine($"{greeting} {name}!");

        if (verbose)
            console.Out.WriteLine($"All done!");
    }

    // helpers

    private static Command WithHandler(this Command command, string methodName)
    {
        var method = typeof(Program).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        var handler = CommandHandler.Create(method!);
        command.Handler = handler;
        return command;
    }
}