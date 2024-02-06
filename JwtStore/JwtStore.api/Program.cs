using JwtStore.api.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();


builder.AddAccountContext();
//app.AddStoreEndpoints();
//app.AddBackOfficeEndpoints();

builder.AddMediatR();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountEndpoints();
//app.MapStoreEndpoints();
//app.MapBackOfficeEndpoints();

app.Run();
