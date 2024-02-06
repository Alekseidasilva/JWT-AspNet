namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
}