namespace JwtAspNet.Models;

public class User
{
    public record User(
        int id, 
        string Password,
        string[] Roles);
}