using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Domain.UserManagement;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Features.Jwt;

public class JwtHandler
{
    private readonly string _jwtValidIssuer;
    private readonly string _jwtValidAudience;
    private readonly string _jwtSecurityKey;
    private readonly double _jwtExpiryInMinutes;

    public JwtHandler()
    {
         Env.Load();

         _jwtValidIssuer = Env.GetString("JWT_ISSUER") 
                           ?? throw new InvalidOperationException("JWT issuer is not configured.");

         _jwtValidAudience = Env.GetString("JWT_AUDIENCE") 
                             ?? throw new InvalidOperationException("JWT audience is not configured.");

         _jwtSecurityKey = Env.GetString("JWT_SECRET") 
                           ?? throw new InvalidOperationException("JWT security key is not configured.");

         if (!double.TryParse(Env.GetString("JWT_EXPIRY_IN_MINUTES"), out _jwtExpiryInMinutes))
         {
             throw new InvalidOperationException("JWT expiry time is not configured correctly.");
         }
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
        var key = Encoding.UTF8.GetBytes(_jwtSecurityKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.FullName.FirstName),
            new Claim(ClaimTypes.Surname, user.FullName.LastName),
        };

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtValidIssuer,
            audience: _jwtValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtExpiryInMinutes),
            signingCredentials: credentials
        );

        return tokenOptions;
    }
}
