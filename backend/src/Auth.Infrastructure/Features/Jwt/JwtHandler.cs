using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Domain.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Features.Jwt;

public class JwtHandler
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;

    public JwtHandler(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection("JWTSettings");
    }

    public string CreateToken(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value!);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.FullName.FirstName),
            new Claim(ClaimTypes.Surname, user.FullName.LastName),
        };

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
    {
        var validIssuer = _jwtSettings.GetSection("validIssuer").Value;
        var validAudience = _jwtSettings.GetSection("validAudience").Value;
        var validExpiry = _jwtSettings.GetSection("expiryInMinutes").Value;
        
        if (string.IsNullOrWhiteSpace(validIssuer))
        {
            throw new InvalidOperationException("JWT issuer is not configured properly.");
        }
        
        if (!double.TryParse(validExpiry, out var expiry))
        {
            throw new ArgumentException("Invalid expiry time configuration for JWT.");
        }
        
        var tokenOptions = new JwtSecurityToken(
            issuer: validIssuer,
            audience: validAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(expiry),
            signingCredentials: credentials
        );

        return tokenOptions;
    }
}