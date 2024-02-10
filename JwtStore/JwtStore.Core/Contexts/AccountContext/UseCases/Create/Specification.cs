using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    //Trata a questão de Contratos
    public static Contract<Notification> Ensure(Request request) => new Contract<Notification>()
        .Requires()
        .IsLowerThan(request.Name.Length, 160, "Name", "O Nome deve ter no máximo 160 caracteres")
        .IsGreaterThan(request.Name.Length, 3, "Name", "O Nome deve ter no mínimo 3 caracteres")
        .IsLowerThan(request.Password.Length, 40, "Password", "A Senha deve ter no máximo 40 caracteres")
        .IsGreaterThan(request.Password.Length, 8, "Password", "O Nome deve ter no mínimo 3 caracteres")
        .IsEmail(request.Email, "Email", "E-mail inválido");

}