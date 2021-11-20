namespace Wit.Core;

public class Author
{
        private string? _name;
        
        private string Name
        {
            get 
            {
                if (string.IsNullOrEmpty(_name))
                    return Environment.GetEnvironmentVariable("GIT_AUTHOR_NAME") ?? string.Empty;
                
                    return _name;
            }
            set => _name = value;
        }

        private string? _email;

        private string Email
        {
            get 
            {
                if (string.IsNullOrEmpty(_email))
                    return Environment.GetEnvironmentVariable("GIT_AUTHOR_EMAIL") ?? string.Empty;
                
                    return _email;
            }
            set => _email = value;
        }
        
    public Author(string? name, string? email)
    {
        Email = email ?? string.Empty;
        Name = name ?? string.Empty;
    }

    public override string ToString()
    {
        var time = DateTime.UtcNow;
        var unixTimestamp = new DateTimeOffset(time).ToUnixTimeMilliseconds();
        return $"{Name} <{Email}> {unixTimestamp} +0000";
    }
}