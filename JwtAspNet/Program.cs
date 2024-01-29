using System.Security.Claims;
using System.Text;
using JwtAspNet;
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
       id=user.Claims.FirstOrDefault(x=>x.Type=="id").Value,
       name=user.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.Name).Value,
       email=user.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.Email).Value,
       Givenname=user.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.GivenName).Value,
       image=user.Claims.FirstOrDefault(x=>x.Type=="image").Value,
    })
    .RequireAuthorization();
app.MapGet("/admin", () => "Você tem Acesso!")
    .RequireAuthorization("admin");
app.Run();
