namespace StoreMeDaddy.Objects;
using System.Linq;
using HashSlingingSlasher;

public record UserRecord(string Username, string Role)
{
}

public record NewUserRecord(string Username, string Role, byte[] Hash, byte[] Salt) : UserRecord(Username, Role)
{
    private static readonly string SpecialChars = @"@%!#$%^&*()?/><,|}]{~`+=";
    public NewUserRecord(string username, string role, string password) : this(username, role, Array.Empty<byte>(), Array.Empty<byte>())
    {
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
        Role = role;
        Hash = hash;
        Salt = salt;
    }
}

public record ExistingUserRecord(int Id, string Username, string Role, byte[] Hash, byte[] Salt) : NewUserRecord(Username, Role, Hash, Salt)
{
}