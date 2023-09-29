
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
}
