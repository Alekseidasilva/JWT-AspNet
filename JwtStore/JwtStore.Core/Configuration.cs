namespace JwtStore.Core;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();
    public static DatabaseConfiguration Database { get; set; } = new();
    public class DatabaseConfiguration
    {
        public string ConnectionStrings { get; set; }=String.Empty;
    }

   
    public class SecretsConfiguration
    {
        public string ApiKey { get; set; }=String.Empty;
        public string JwtPrivateKey { get; set; }=String.Empty;
        public string PasswordSaltKey { get; set; }=String.Empty;
        
        
    }
}