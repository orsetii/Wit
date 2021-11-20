
using Wit.Services;
using Wit.Utils;

namespace Wit.Data;

public class Tree : IGitObject
{
    private List<Entry> _entries;
    
    public List<Entry> Entries
    {
        get => _entries;
        init => _entries = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public GitObjectType GitType => GitObjectType.Tree;
    
    public byte[] Data => GetData();
    
    
    public Tree()
    {
        Entries = new List<Entry>();
    }
    
    public Tree(List<Entry> entries)
    {
        Entries = entries;
    }

    private byte[] GetData()
    {
        IEnumerable<byte> data = Array.Empty<byte>();
        Entries.ForEach(e =>
        {
            data = data.Concat(e.AsEncoded());
        });
        return data.ToArray();
    }
}
