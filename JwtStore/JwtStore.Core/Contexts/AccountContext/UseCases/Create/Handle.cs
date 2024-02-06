using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

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
        #endregion 
        #region 03. Verificar se o usuario exisite
        #endregion 
        #region 04. Persistir os dados
        #endregion 
        #region 05. Enviar Email de Activação
        #endregion 
    }
}