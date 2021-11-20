
namespace Wit.Services
{
    public interface IGitObject
    {
        public GitObjectType GitType { get; }
        public byte[] Data { get; }
    }

    public class GitObjectType
    {
        private GitObjectType(string value) { Value = value; }

        public string Value { get; }

        public static GitObjectType Blob => new("blob");
        public static GitObjectType Tree => new("tree");
        public static GitObjectType Entry => new("entry");
        public static GitObjectType Commit => new("commit");

        public override string ToString()
        {
            return Value;
        }
    }

}
