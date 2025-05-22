using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public static class IdentityExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        return userId;
    }

    private static readonly JwtSecurityTokenHandler JWTSecurityTokenHandler = new JwtSecurityTokenHandler();
    public static string GenerateJwt(
        IEnumerable<Claim> claimsPrincipalClaims, 
        string key, 
        string issuer, 
        string audience, 
        int jwtExpiresInSeconds)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claimsPrincipalClaims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(jwtExpiresInSeconds), 
            signingCredentials: signingCredentials
        );

        
        return JWTSecurityTokenHandler.WriteToken(token);

    }

    public static bool ValidateJWT(string jwt, string key, string issuer, string audience)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            handler.ValidateToken(jwt, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

                ValidateLifetime = false
            }, out SecurityToken validatedToken);

            return validatedToken is JwtSecurityToken;
        }
        catch
        {
            return false;
        }
    }
}