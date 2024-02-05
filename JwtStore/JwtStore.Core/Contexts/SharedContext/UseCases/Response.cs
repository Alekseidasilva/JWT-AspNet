using Flunt.Notifications;

namespace JwtStore.Core.Contexts.SharedContext.UseCases;

public abstract class Response
{
    public string Message { get; set; }=String.Empty;
    public int Status { get; set; } = 400;
    public bool IsSucess=>Status is >200 and <=299;
    public IEnumerable<Notification>?Notifications { get; set; }
}