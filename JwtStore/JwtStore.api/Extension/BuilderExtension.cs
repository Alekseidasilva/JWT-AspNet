using System.Text;
using JwtStore.Core;
using JwtStore.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JwtStore.api.Extension;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection") ?? String.Empty;
        Configuration.Secrets.ApiKey = builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? String.Empty;
        Configuration.Secrets.JwtPrivateKey = builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? String.Empty;
        Configuration.Secrets.PasswordSaltKey = builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltkey") ?? String.Empty;

        Configuration.SendGrid.ApiKey = builder.Configuration.GetSection("SendGrid").GetValue<string>("ApiKey") ?? String.Empty;


        Configuration.Email.DefaultFromName = builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromName") ?? String.Empty;
        Configuration.Email.DefaultFromEmail = builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromEmail") ?? String.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
            x.UseSqlServer(Configuration.Database.ConnectionStrings, b => b.MigrationsAssembly("JwtStore.api")));
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.ApiKey)),
                    ValidateIssuer = false,
                };
            });
        builder.Services.AddAuthorization();

    }

    public static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x
            => x.RegisterServicesFromAssembly(typeof(Configuration).Assembly));
    }

}