namespace StoreMeDaddy.Objects;

public class TokenSettings
{
    public string SecretKey { get; init; }
    public string Issuer { get; init; }
    public int ExpiryMinutes { get; init; }
    public List<string> Roles { get; init; } 

    public TokenSettings(string secretKey, string issuer, int expiryMinutes, List<string>? roles)
    {
        SecretKey = secretKey;
        Issuer = issuer;
        ExpiryMinutes = expiryMinutes;
        Roles = roles ?? new() { "User" } ;
    }
}
