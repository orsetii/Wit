using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wit.Cli.Commands;
using Wit.Data;
using Wit.Utils;
using Xunit;

namespace Wit.Test;

public class DatabaseTest
{
    private string tempFolder;

    public DatabaseTest()
    {
        tempFolder = Path.Combine(Path.GetTempPath(), "tmp_wit_"+ Guid.NewGuid());
        
        // generate new git repo in random temp folder.
        var ic = new InitCommand();
        ic.Handle(tempFolder);
        Console.WriteLine(Directory.EnumerateFileSystemEntries(tempFolder).ToList());
        
        // Create file to test commit compression with
        var f = File.Create(Path.Combine(tempFolder, "test.cs"));
        f.Write(Encoding.Default.GetBytes("testing wow yes that is poggerss!"));
        f.Close();

    }
    
    [Fact]
    public void WriteObjectCorrectCompression()
    {
        var cc = new CommitCommand(tempFolder);
        cc.Handle(null, null);
        var files = Directory.GetFiles(Path.Combine(tempFolder, ".git"), "*.*", SearchOption.AllDirectories);
        var db = Database.ReadCompressedFile(files[0]);
    }

    [Fact]
    public void stringrepr()
    {
        var entryList = new List<Entry>();
        
        entryList.Add(new Entry("pisswaffle", new Oid("bob")));
        entryList.Add(new Entry("burgerking", new Oid("askmgjnajkgnajkdgn")));
        var cls = new Tree(entryList);
        var res = cls.Data;
    }
    
}