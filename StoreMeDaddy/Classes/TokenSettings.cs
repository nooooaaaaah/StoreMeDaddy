namespace StoreMeDaddy.Classes;

public class TokenSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public int ExpiryMinutes { get; set; }

    public TokenSettings(string secretKey, string issuer, int expiryMinutes)
    {
        SecretKey = secretKey;
        Issuer = issuer;
        ExpiryMinutes = expiryMinutes;
    }
}
