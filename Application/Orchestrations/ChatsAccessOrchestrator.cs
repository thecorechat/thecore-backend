using Application.Orchestrations.Interfaces;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orchestrations
{
    public sealed class ChatsAccessOrchestrator : IChatsAccessOrchestrator
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public ChatsAccessOrchestrator(IServiceScopeFactory serviceScopeFactory) 
        { 
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public Task<TResult> ExecuteAsync<TResult>(Func<IChatsAccessService, Task<TResult>> action)
        {
            var scope = serviceScopeFactory.CreateScope();

            var chatsAccessService = scope.ServiceProvider.GetRequiredService<IChatsAccessService>();

            return action(chatsAccessService);
        }
    }
}
