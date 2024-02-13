using System.Security.Cryptography.X509Certificates;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    => _repository = repository;


    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01.Valida a Requisição.

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

        #region 02. Recupera o Perfil

        User? user;
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Perfil não encontrado", 404);
        }
        catch (Exception e)
        {
            return new Response("Não foi possivel recuperar o seu perfil", 500);
        }

        #endregion

        #region 03.Verificar se a Senha é Valida

        if (!user.Password.Challenge(request.Password))
            return new Response("Usuário ou senha inválidos", 400);

        #endregion

        #region 04.Checar se a conta está verificada

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Conta Inativa", 400);
        }
        catch
        {
            return new Response("Não foi possivel verificar o seu perfil", 500);
        }

        #endregion


        #region 05. retona os Dados

        try
        {
            var data = new Response.ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };
            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possivel obter os dados do Perfil", 500);
        }

        #endregion

    }
}