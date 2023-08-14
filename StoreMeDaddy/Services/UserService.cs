namespace StoreMeDaddy.Services;
using StoreMeDaddy.Models;
using HashSlingingSlasher;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public interface IUserService
{
    Task<UserModel> Authenticate(string username, string password);
    Task<UserModel> CreateUserAsync(UserModel user, string password);
    Task<UserModel> DeleteUserAsync(ClaimsPrincipal user);
    Task<UserModel> GetUserAsync(ClaimsPrincipal user);
    Task<UserModel> UpdateUserAsync(UserModel user);
}
public class UserService : IUserService

{
    private readonly StoreMeDaddyContext _context;
    public UserService(StoreMeDaddyContext context)
    {
        _context = context;
    }
    public async Task<UserModel> Authenticate(string username, string password)
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
        return user;
    }
    public async Task<UserModel> GetUserAsync(ClaimsPrincipal principal)
    {
        // Extract the user ID from the claims
        string userIdClaim = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("User ID claim not found in principal.");
        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new Exception("User ID claim could not be parsed to an integer.");
        }

        // Retrieve the user by ID from the database
        UserModel user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("User not found in the database.");
        return user;
    } 
    public async Task<UserModel> UpdateUserAsync(UserModel user)
    {
        if (user == null)
            throw new Exception("User is null");
        if (!await _context.Users.AnyAsync(x => x.Id == user.Id))
        {
            throw new Exception("User not found");
        }
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<UserModel> DeleteUserAsync(ClaimsPrincipal principal)
    {
        string userIdClaim = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("User ID claim not found in principal.");
        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new Exception("User ID claim could not be parsed to an integer.");
        }
        UserModel userModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new Exception("User not found");
        _context.Users.Remove(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }

    public async Task<UserModel> CreateUserAsync(UserModel user, string password)
    {
        if (user == null)
            throw new Exception("User is null");
        if (await _context.Users.AnyAsync(x => x.Username == user.Username))
        {
            throw new Exception("Username already exists");
        }
        PasswordHasher.CreatePasswordHash(password, out byte[] hash, out byte[] salt);
        user.Hash = hash;
        user.Salt = salt;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}