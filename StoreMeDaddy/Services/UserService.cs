namespace StoreMeDaddy.Services;
using StoreMeDaddy.Models;
using HashSlingingSlasher;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using StoreMeDaddy.Objects;

public interface IUserService
{
    Task<ExistingUserRecord> Authenticate(string username, string password);
    Task<ExistingUserRecord> CreateUserAsync(UserRecord user, string password);
    Task<ExistingUserRecord> DeleteUserAsync(ClaimsPrincipal user);
    Task<ExistingUserRecord> GetUserAsync(ClaimsPrincipal user);
    Task<ExistingUserRecord> UpdateUserAsync(ExistingUserRecord user);
}
public class UserService : IUserService

{
    private readonly StoreMeDaddyContext _context;
    public UserService(StoreMeDaddyContext context)
    {
        _context = context;
    }
    public async Task<ExistingUserRecord> Authenticate(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new Exception("Username or password is empty");
        }
        if (!await _context.Users.AnyAsync(x => x.Username == username))
        {
            throw new Exception("Username not found");
        }
        UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username) ?? throw new Exception("User not found");
        if (!PasswordHasher.VerifyPasswordHash(password, user.Hash, user.Salt))
            throw new Exception("Incorrect password");
        return new ExistingUserRecord(user.Id, user.Username, user.Role, user.Hash, user.Salt);
    }
    public async Task<ExistingUserRecord> GetUserAsync(ClaimsPrincipal principal)
    {
        // Extract the user ID from the claims
        string userIdClaim = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("User ID claim not found in principal.");
        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new Exception("User ID claim could not be parsed to an integer.");
        }

        // Retrieve the user by ID from the database
        UserModel user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found in the database.");
        return new ExistingUserRecord(user.Id, user.Username, user.Role, user.Hash, user.Salt);
    } 
    public async Task<ExistingUserRecord> UpdateUserAsync(ExistingUserRecord user)
    {
        if (user == null)
            throw new Exception("User is null");
        if (!await _context.Users.AnyAsync(x => x.Id == user.Id))
        {
            throw new Exception("User not found");
        }
        UserModel userModel = new()
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role,
            Hash = user.Hash,
            Salt = user.Salt
        };
        _context.Users.Update(userModel);
        await _context.SaveChangesAsync();
        return new ExistingUserRecord(userModel.Id, userModel.Username, userModel.Role, userModel.Hash, userModel.Salt);
    }
    public async Task<ExistingUserRecord> DeleteUserAsync(ClaimsPrincipal principal)
    {
        string userIdClaim = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("User ID claim not found in principal.");
        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new Exception("User ID claim could not be parsed to an integer.");
        }
        UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new Exception("User not found");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return new ExistingUserRecord(user.Id, user.Username, user.Role, user.Hash, user.Salt); ;
    }

    public async Task<ExistingUserRecord> CreateUserAsync(UserRecord user, string password)
    {
        if (user == null)
            throw new Exception("User is null");
        if (await _context.Users.AnyAsync(x => x.Username == user.Username))
        {
            throw new Exception("Username already exists");
        }
        NewUserRecord newUserRecord = new(user.Username, user.Role, password);
        UserModel newUser = new()
        {
            Username = newUserRecord.Username,
            Role = newUserRecord.Role,
            Hash = newUserRecord.Hash,
            Salt = newUserRecord.Salt
        };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return new(newUser.Id, newUser.Username, newUser.Role, newUser.Hash, newUser.Salt);
    }
}