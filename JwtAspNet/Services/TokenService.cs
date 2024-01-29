using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
}