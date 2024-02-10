using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtStore.Core;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;

namespace JwtStore.api.Extension;

public class JwtExtension
{
    public static string Generate(Response.ResponseData data)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokendescritptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(data),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials
        };
        var token = handler.CreateToken(tokendescritptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Response.ResponseData user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim("Id", user.Id));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        return ci;
    }
}