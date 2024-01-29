using System.Security.Claims;
using System.Text;
using JwtAspNet;
using JwtAspNet.Extensions;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
      ValidateIssuer = false,
      ValidateAudience = false
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("admin",p=>p.RequireRole("admin"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (TokenService service) =>
{
     var user=new Users.User(
          1,
          "Aleksei da Silva",
          "alekseidasilva@gmail.com",
          "https://ajmdstec.ao",
          "049222Xp12",new string[] { "Student","Premium" }
     );
    return service.Create(user);
});
app.MapGet("/restrito", (ClaimsPrincipal user) =>new
    {
       id=user.Id(),
       name=user.Name(),
       email=user.Email(),
       Givenname=user.GivenName(),
       image=user.Image(),
    })
    .RequireAuthorization();
app.MapGet("/admin", () => "Você tem Acesso!")
    .RequireAuthorization("admin");
app.Run();
