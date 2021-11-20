namespace Wit.Core;

public class Author
{
        private string? _name;
        
        private string Name
        {
            get 
            {
                if (string.IsNullOrEmpty(_name))
                    return _name;
                
                return Environment.GetEnvironmentVariable("GIT_AUTHOR_NAME") ?? string.Empty;
            }
            set => _name = value;
        }

        private string? _email;

        private string Email
        {
            get 
            {
                if (string.IsNullOrEmpty(_email))
                    return _email ;
                
                return Environment.GetEnvironmentVariable("GIT_AUTHOR_EMAIL") ?? string.Empty;
            }
            set => _email = value;
        }
        
    public Author(string? name, string? email)
    {
        Email = email ?? string.Empty;
        Name = name ?? string.Empty;
    }

}