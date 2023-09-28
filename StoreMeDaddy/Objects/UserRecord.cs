namespace StoreMeDaddy.Objects;
using System.Linq;
using HashSlingingSlasher;

public record UserRecord(string Username, string Role, byte[] Hash, byte[] Salt)
{
    private static readonly string SpecialChars = @"@%!#$%^&*()?/><,|}]{~`+=";

    public UserRecord(string username, string password, string role) : this("", "", Array.Empty<byte>(), Array.Empty<byte>())
    {
        if (username == null)
        {
            throw new ArgumentNullException(nameof(username));
        }
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        if (username.Length < 3 || username.Any(SpecialChars.Contains))
        {
            throw new ArgumentException("Username must be at least 3 characters long and must not contain any special characters.");
        }
        if (password.Length < 3 || !password.Any(SpecialChars.Contains))
        {
            throw new ArgumentException("Password must be at least 3 characters long and must contain at least one special character.");
        }

        PasswordHasher.CreatePasswordHash(password, out byte[] hash, out byte[] salt);
        Username = username;
        Salt = salt;
        Hash = hash;
        Role = role;
    }
}