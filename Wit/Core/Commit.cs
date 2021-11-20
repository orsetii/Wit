using System.Text;
using Wit.Services;
using Wit.Utils;

namespace Wit.Core;

public class Commit : IGitObject
{
    public GitObjectType GitType => GitObjectType.Commit;
    public byte[] Data => AsEncoded();

    private Oid _treeOid;
    
    private Oid _oid;
    public Oid Oid
    {
        get => _oid;
        set => _oid = value;
    }

    private Author _author;
    private Author Author
    {
        get => _author;
        set => _author = value;
    }

    private string _message;
    public string Message
    {
        get => _message;
        set => _message = value;
    }

    public Commit(Oid treeOid, Author author, string message)
    {
        Message = message;
        _treeOid = treeOid;
        Author = author;
    }


    private byte[] AsEncoded()
    {
        var ret = "tree ";
        ret = string.Concat(ret, _treeOid);
        return Encoding.Default.GetBytes(ret + $"tree {_treeOid}\nauthor {Author}\ncommitter {Author}\n\n{Message}");
    }
}