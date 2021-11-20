using System.CommandLine.Invocation;
using System.CommandLine;
using Wit.Services;

namespace Wit.Cli.Commands;



public class InitCommand : ICliCommand
{
    private string _dir = "";

    private readonly IDirectoryManager _directoryManager;

    public InitCommand(IDirectoryManager directoryManager)
    {
        _directoryManager = directoryManager;
    }

    public InitCommand()
    {
        _directoryManager = new DirectoryManager();
    }

    public Command CreateCommand()
    {
        var cmd = new Command("init", "Initialize a new repository.");

        cmd.Add(new Option("--dir", "Initialize a new repository", typeof(string), () => Directory.GetCurrentDirectory()));

        cmd.Handler = CommandHandler.Create(Handle);

        return cmd;
    }

    public void Handle(string dir)
    {
        _dir = Path.GetFullPath(dir);
        // The `dir` passed in here will be either the path specified in cli or the CWD as is the default value.

        CreateGitDirectories();
        Console.WriteLine($"Initialized empty Wit repository in {_dir}");
    }

    private void CreateGitDirectories()
    {

        // Generate and create the git directory
        var gitDirPath = Path.Combine(_dir, ".git");

        _directoryManager.Create(gitDirPath);

        _directoryManager.Create(Path.Combine(gitDirPath, "objects"));
        _directoryManager.Create(Path.Combine(gitDirPath, "refs"));
    }

}