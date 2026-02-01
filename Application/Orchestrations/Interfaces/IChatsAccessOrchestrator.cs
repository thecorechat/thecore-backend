using Application.Services.Interfaces;

namespace Application.Orchestrations.Interfaces
{
    public interface IChatsAccessOrchestrator
    {
        Task<TResult> ExecuteAsync<TResult>(Func<IChatsAccessService, Task<TResult>> action);
    }
}
