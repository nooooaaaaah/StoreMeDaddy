
namespace StoreMeDaddy.Models;
using System;
using System.Linq;
using HashSlingingSlasher;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public byte[] Hash { get; set; }
    public byte[] Salt { get; set; }

    private static readonly string SpecialChars = @"@%!#$%^&*()?/><,|}]{~`+=";
    protected UserModel()
    {
        Username = string.Empty;
        Hash = Array.Empty<byte>();
        Salt = Array.Empty<byte>();
        Role = string.Empty;
    }
    public UserModel(string username, string password, string role)
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
