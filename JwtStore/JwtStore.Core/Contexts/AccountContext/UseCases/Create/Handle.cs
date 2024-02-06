using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.ValueObjects;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handle//Manipulador
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handle(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição Inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possivel validar sua requisição", 500);
        }
        #endregion 
        #region 02. Gerar od Objectos

        Email email;
        Password password;
        User user;
        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        #endregion 
        #region 03. Verificar se o usuario exisite no banco

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este email já esta em uso", 400);

        }
        catch
        {
            return new Response("Falha ao verificar o Email cadastrado", 500);
        }

        #endregion
        #region 04. Persistir os dados

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Façha ao persistir os dados", 500);
        }
        #endregion
        #region 05. Enviar Email de Activação

        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            //Do Not Nothing
        }
        #endregion

        return new Response("Contra criada com Sucesso", new Response.ResponseData(user.Id, user.Name, user.Email));
    }
}