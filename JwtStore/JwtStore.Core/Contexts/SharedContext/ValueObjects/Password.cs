using System.Security.Cryptography;

namespace JwtStore.Core.Contexts.SharedContext.ValueObjects;


public class Password : ValueObject
{
    protected Password()
    {

    }
    public Password(string? text = null)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            text = Generate();
        Hash = Hashing(text);
    }
    private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    private const string Special = "!@#$%^&*(){}[];";

    public string Hash { get; private set; } = string.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

    private static string Generate(short length = 16,
        bool includeSpecialChars = true,
        bool upperCase = false)
    {
        var chars = includeSpecialChars ? (Valid + Special) : Valid;
        var startRandom = upperCase ? 26 : 0;
        var index = 0;
        var res = new char[length];
        var rnd = new Random();

        while (index < length)
            res[index++] = chars[rnd.Next(startRandom, chars.Length)];
        return new string(res);
    }

    private static string Hashing(string password,
        short saltSize = 16,
        short KeySize = 32,
        int iterations = 1000,
        char splitChar = '.')
    {
        if (string.IsNullOrEmpty(password))
            throw new Exception("Password should not be bull or empty");
        password += Configuration.Secrets.PasswordSaltKey;

        using var algoritm = new Rfc2898DeriveBytes(password, saltSize, iterations, HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algoritm.GetBytes(KeySize));
        var salt = Convert.ToBase64String(algoritm.Salt);

        return $"{iterations}{splitChar}{salt}{splitChar}{key}";
    }

    private static bool Verify(
        string hash,
        string password,
        short keySize = 32,
        int iterations = 1000,
        char splitChar = '.')
    {
        password += Configuration.Secrets.PasswordSaltKey;
        var parts = hash.Split(splitChar, 3);
        if (parts.Length != 3)
            return false;

        var hashIterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);
        if (hashIterations != iterations)
            return false;

        using var algoritm = new Rfc2898DeriveBytes(
            password, salt, iterations, HashAlgorithmName.SHA256);

        var keyToCheck = algoritm.GetBytes(keySize);
        return keyToCheck.SequenceEqual(key);
    }

    public bool Challenge(string plainTextPassword)
        => Verify(Hash, plainTextPassword);
}