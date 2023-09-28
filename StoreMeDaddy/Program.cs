using CoinMachine;
using StoreMeDaddy.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StoreMeDaddy.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.DataProtection;
using StoreMeDaddy.Models;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<ITokenService>(provider =>
{
    TokenSettings tokenSettings = provider.GetRequiredService<IOptions<TokenSettings>>().Value;
    ILogger<TokenService> logger = provider.GetRequiredService<ILogger<TokenService>>();
    return new TokenService(tokenSettings.SecretKey, tokenSettings.Issuer, tokenSettings.ExpiryMinutes, tokenSettings.Roles);
});
builder.Services.AddDbContext<StoreMeDaddyContext>(options =>
{
    options.UseSqlite("Data Source=storemedaddy.db");
});

// builder.Services.AddDataProtection()
//     .PersistKeysToDbContext<StoreMeDaddyContext, DataProtectionKey>()
//     .ProtectKeysWithDpapi();

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
string secretKeyConfig = builder.Configuration["TokenSettings:SecretKey"] ?? throw new InvalidOperationException("Secret key must be defined in the configuration.");




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["TokenSettings:Issuer"],
            ValidAudience = builder.Configuration["TokenSettings:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyConfig)),
            LifetimeValidator = (DateTime? before, DateTime? expires, SecurityToken token, TokenValidationParameters parameters) =>
            {
                if (expires.HasValue)
                {
                    return expires > DateTime.UtcNow;
                }
                return false;
            }
        };
    });

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
