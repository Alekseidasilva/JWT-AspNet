namespace JwtStore.api.Extension;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Repository>();

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Service>();
        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Create
        //Mediator-> Patern que faz o intermedio entre as comunicações


        #endregion
    }
}