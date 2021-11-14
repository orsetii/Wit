using System.CommandLine.Invocation;
using System.CommandLine;
using Wit.Services;

namespace Wit.App.Cli.Commands;



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

        cmd.Add(new Option("--dir", "Intialize a new repository", typeof(string), () => Directory.GetCurrentDirectory()));

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
        var gitDirPath = CreateDirUsingDirectoryArgument(_dir, ".git");

        // Create objects directory
        CreateDirUsingDirectoryArgument(gitDirPath, "objects");

        // Create refs directory
        CreateDirUsingDirectoryArgument(gitDirPath, "refs");
    }

    private string CreateDirUsingDirectoryArgument(string dir, string toAppendPath)
    {
        var path = Path.Combine(dir, toAppendPath);
        _directoryManager.Create(path);
        return path;
    }

}