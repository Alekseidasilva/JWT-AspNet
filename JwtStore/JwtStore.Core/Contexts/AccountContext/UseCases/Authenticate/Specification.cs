using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Specification
{
    //Trata a questão de Contratos
    public static Contract<Notification> Ensure(Request request) =>
        new Contract<Notification>()
        .Requires()
        .IsLowerThan(request.Password.Length, 40, "Password", "A Senha deve ter no máximo 40 caracteres")
        .IsGreaterThan(request.Password.Length, 8, "Password", "O Nome deve ter no mínimo 3 caracteres")
        .IsEmail(request.Email, "Email", "E-mail inválido");

}