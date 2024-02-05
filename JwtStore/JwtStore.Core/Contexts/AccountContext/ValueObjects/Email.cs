using System.Text.RegularExpressions;
using JwtStore.Core.Contexts.SharedContext.Extensions;
using JwtStore.Core.Contexts.SharedContext.ValueObjects;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public partial class Email:ValueObject
{
    protected Email()
    {
        
    }
    private const string Pattern = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
    
    public string Address { get; }
    public string Hash => Address.ToBase64();

    public Verification Verification { get; private set; } = new();


    public void ResendVerification()
    {
        Verification = new Verification();
    }
    
    
    
    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string address) =>new Email(address);
    public override string ToString() => Address;


    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("Email Inválido");
        
        address = address.Trim().ToLower();
        
        if (address.Length < 5)
            throw new Exception("Email Inválido");
        if (!EmailRegex().IsMatch(address))
            throw new Exception("Email Inválido");
    }
}