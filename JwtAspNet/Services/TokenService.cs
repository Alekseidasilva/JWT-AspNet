using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAspNet.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtAspNet.Services;

public class TokenService
{
    public string Create()
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

      var credentials=  new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);
      var tokenDescription = new SecurityTokenDescriptor
      {
          SigningCredentials = credentials,
          Expires = DateTime.UtcNow.AddHours(2),
      };
      
      new Claim(ClaimTypes.Name, "");
      new Claim(ClaimTypes.Email, "");
      new Claim(ClaimTypes.GivenName, "");
      new Claim(ClaimTypes.Role, "");
      
        
        var token = handler.CreateToken(tokenDescription);
        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(Users.User user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim("Id",user.id.ToString()));//Customized Claim
        ci.AddClaim(new Claim(ClaimTypes.Name,user.Email)); //Default Aspnet Claim
        ci.AddClaim(new Claim(ClaimTypes.Email,user.Email));
        ci.AddClaim(new Claim(ClaimTypes.GivenName,user.Name));
        ci.AddClaim(new Claim("Image",user.Image));//Customized Claim
        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role,role));
        
        return ci;
    }
}