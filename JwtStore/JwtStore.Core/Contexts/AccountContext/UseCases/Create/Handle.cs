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

    public async Task<Response> Handle(Request request,CancellationToken token)
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
        #region 03. Verificar se o usuario exisite
        #endregion 
        #region 04. Persistir os dados
        #endregion 
        #region 05. Enviar Email de Activação
        #endregion 
    }
}