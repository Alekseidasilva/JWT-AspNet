using JwtAspNet.Models;
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (TokenService service) =>
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

app.Run();
