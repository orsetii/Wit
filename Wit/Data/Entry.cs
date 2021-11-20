using System.Text;
using Wit.Services;
using Wit.Utils;

namespace Wit.Data;

public class Entry
{

    private string _name;

    public string Name
    {
        get => _name;
        init => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    private Oid _oid;
    public Oid Oid => _oid;
    
    public GitObjectType GitType => GitObjectType.Entry;
    
    public Entry(string pName, Oid oid)
    {
        Name = pName;
        _oid = oid;
    }

    public byte[] AsEncoded()
    {
        return Encoding.Default.GetBytes($"100644 {Name}\0").Concat(Oid.Data).ToArray();
    }
}