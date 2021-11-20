using System.Security.Cryptography;
using System.Text;

namespace Wit.Utils;

public class Oid
{
    private byte[] _oid { get; set; }

    public byte[] Data => _oid;

    /// <summary>
    /// Creates a new Oid with either the Hash or a byte array to be hashed.
    /// </summary>
    /// <param name="b">The bytes to use or hash</param>
    /// <param name="isHash">Is the passed bytes a hash already?</param>
    public Oid(byte[] b, bool isHash)
    {
        _oid = SHA1.Create().ComputeHash(b);
    }
    
    public Oid(string s)
    {
        _oid = SHA1.Create().ComputeHash(Encoding.Default.GetBytes(s));
    }

    public override string ToString()
    {
        return Encoding.ASCII.GetString(_oid);
    }
}