namespace JwtStore.Core.Contexts.SharedContext.ValueObjects;

public class Verification:ValueObject
{
    public Verification()
    {
        
    }
    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; }=DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;

    public void Verify(string code)
    {
        if (IsActive)
            throw new Exception("Este item já foi activado!");
        if (ExpiresAt<DateTime.UtcNow)
            throw new Exception("Este código ja expirou!");
        if (!string.Equals(code.Trim(),Code.Trim(),StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de verificação Inválido!");

        ExpiresAt = null;
        VerifiedAt=DateTime.UtcNow; 
    }

}