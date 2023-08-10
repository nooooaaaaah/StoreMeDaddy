namespace StoreMeDaddy.Models;
public class AuthenticationModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public AuthenticationModel(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentNullException(nameof(username));
        }
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }
        Username = username;
        Password = password;
    }
}
