using System.Runtime.InteropServices.ComTypes;

namespace JwtStore.Core.Contexts.AccountContext.Entities;

public class Role : JwtStore.Core.Contexts.SharedContext.Entities.Entity
{
    public string Name { get; set; }=String.Empty;

    #region Users

    public List<User> Users { get; set; } = new();


    #endregion
}