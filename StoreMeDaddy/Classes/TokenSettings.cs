namespace StoreMeDaddy.Classes;

public class TokenSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public int ExpiryMinutes { get; set; }
    public List<string> Roles { get; set; } 

    public TokenSettings(string secretKey, string issuer, int expiryMinutes, List<string>? roles)
    {
        SecretKey = secretKey;
        Issuer = issuer;
        ExpiryMinutes = expiryMinutes;
        Roles = roles ?? new() { "User" } ;
    }
}
