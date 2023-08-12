namespace StoreMeDaddy.Services;
using StoreMeDaddy.Models;
using HashSlingingSlasher;
using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    Task<UserModel> Authenticate(string username, string password);
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
            throw new Exception("Username or password is empty");
        if (!await _context.Users.AnyAsync(x => x.Username == username))
        {
            throw new Exception("Username not found");
        }
        UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username) ?? throw new Exception("User not found");
        if (!PasswordHasher.VerifyPasswordHash(password, user.Hash, user.Salt))
            throw new Exception("Incorrect password");
        return user;
    }
}