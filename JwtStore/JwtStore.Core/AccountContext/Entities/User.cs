using JwtStore.Core.SharedContext.Entities;

namespace JwtStore.Core.AccountContext.Entities;

public class User:Entity
{
    public string Email { get; set; }
}