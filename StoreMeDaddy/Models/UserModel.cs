namespace StoreMeDaddy.Models;
using System;
using System.Linq;
using System.Text;
using HashSlingingSlasher;

public class UserModel
{
    public string Username { get; set; }
    public byte[] Hash { get; set; }
    public byte[] Salt { get; set; }

    private static readonly string SpecialChars = @"@%!#$%^&*()?/><,|}]{~`+=";

    public UserModel(string username, string password)
    {
        if (username == null)
        {
            throw new ArgumentNullException(nameof(username));
        }
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        if (username.Length < 3 || username.Any(u => SpecialChars.Contains(u)))
        {
            throw new ArgumentException("Username must be at least 3 characters long and must not contain any special characters.");
        }
        if (password.Length < 3 || !password.Any(u => SpecialChars.Contains(u)))
        {
            throw new ArgumentException("Password must be at least 3 characters long and must contain at least one special character.");
        }

        PasswordHasher.CreatePasswordHash(password, out byte[] salt, out byte[] hash);
        Username = username;
        Salt = salt;
        Hash = hash;
    }
}
