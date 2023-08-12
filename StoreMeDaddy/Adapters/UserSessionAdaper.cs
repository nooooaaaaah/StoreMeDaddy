namespace StoreMeDaddy.Adapters;
using CoinMachine;
using StoreMeDaddy.Models;

public class UserSessionAdapter : IUser
{
    private readonly UserModel _user;

    public UserSessionAdapter(UserModel user)
    {
        _user = user;

    }
    public int UserID => _user.Id;
    public string Username => _user.Username;
    public string Role => _user.Role;
}
