namespace JwtAspNet.Models;

public class Users
{
    public record User(
        int id, 
        string Name,
        string Email,
        string Image,
        string Password,
        string[] Roles);
}