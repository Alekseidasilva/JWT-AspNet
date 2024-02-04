namespace JwtStore.Core;

public static class Configuration
{
    public static SecretsConfiguration screts { get; set; } = new();
    public class SecretsConfiguration
    {
        public string ApiKey { get; set; }=String.Empty;
        public string JwtPrivateKey { get; set; }=String.Empty;
        public string PasswordSaltKey { get; set; }=String.Empty;
        
        
    }
}