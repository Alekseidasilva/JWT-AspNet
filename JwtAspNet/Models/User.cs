namespace JwtAspNet.Models;

public class User
{
    public record user(
        int id, 
        string Password,
        string[] Roles);
}