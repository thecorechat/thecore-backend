

using Domain.Interfaces;
using Domain.Models;
using Domain.Records;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using MR.AspNetCore.Pagination;
using MR.EntityFrameworkCore.KeysetPagination;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repositories
{
    internal class ChatsQueryRepository : IChatsQueryRepository
    {
        private SchoolChatContext Context { get; init; }
        private IPaginationService PaginationService { get; init; }

        public ChatsQueryRepository(SchoolChatContext schoolChatContext, IPaginationService paginationService)
        {
            Context = schoolChatContext;
            PaginationService = paginationService;
        }
        public async Task<IEnumerable<Chat>> ChatsContainsTheUser(int userId)
        {
            List<Chat> result = await Context.Chats
                .Where(c => c.Users.Any(u => u.Id == userId))
                .ToListAsync();
            return result;
        }

        public async Task<KeysetPaginationAfterResult<Chat>> GetChatKeysetPaginationAsync(string? after, PropertyInfo propInf, int limit, bool reverse)
        {
            //var builder = KeysetQuery.Build<Chat>(actions =>
            //{
            //    {
            //        Expression<Func<Chat, object>> prop = Expression.Lambda<Func<Chat, object>>(
            //            Expression.Convert(
            //                Expression.Property(Expression.Parameter(typeof(Chat), "c"), propInf),
            //                typeof(object)
            //            ),
            //            Expression.Parameter(typeof(Chat), "c")
            //        );


            //        actions.Descending(prop)
            //               .Descending(c => c.Id);
            //    }
            //});
            throw new NotImplementedException();
        }

        public Task<KeysetPaginationAfterResult<Chat>> GetChatKeysetPaginationAsync(string? after, string propName, int limit, bool reverse)
        {
            PropertyInfo propInfo = typeof(Chat).GetProperties().Single(p => p.Name == propName);
            return GetChatKeysetPaginationAsync(after, propInfo, limit, reverse);
        }
    }
}
