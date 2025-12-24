

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
    public class ChatsQueryRepository : IChatsQueryRepository
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
            IQueryable<Chat> query = Context.Chats;

            KeysetQueryModel cursor = new KeysetQueryModel()
            {
                After = after,
                Size = limit
            };

            var keySet = await PaginationService.KeysetPaginateAsync<Chat>(
                query,
                CreateActionKeysetPaginationBuilder(propInf.Name, reverse),
                async id => await Context.Chats.FindAsync(int.Parse(id)),
                queryModel: cursor
            );

            string? newAfter = keySet.Data.LastOrDefault()?.Id.ToString();

            KeysetPaginationAfterResult<Chat> afterResult = new(
                After: newAfter,
                Data: keySet.Data,
                TotalCount: keySet.TotalCount,
                PageSize: keySet.PageSize,
                HasPrevious: keySet.HasPrevious,
                HasNext: keySet.HasNext
            );


            return afterResult;
        }

        public Task<KeysetPaginationAfterResult<Chat>> GetChatKeysetPaginationAsync(string? after, string propName, int limit, bool reverse)
        {
            PropertyInfo propInfo = typeof(Chat).GetProperties().Single(p => p.Name == propName);
            return GetChatKeysetPaginationAsync(after, propInfo, limit, reverse);
        }

        private Action<KeysetPaginationBuilder<Chat>> CreateActionKeysetPaginationBuilder(string? propName, bool? reverse)
        {
            var parameter = Expression.Parameter(typeof(Chat), "m");
            var property = Expression.PropertyOrField(parameter, propName ?? nameof(Chat.Id));
            var propertyType = property.Type;

            var lambdaType = typeof(Func<,>).MakeGenericType(typeof(Chat), propertyType);
            var lambda = Expression.Lambda(lambdaType, property, parameter);

            return builder =>
            {
                var ascendingMethod = typeof(KeysetPaginationBuilder<Chat>)
                    .GetMethods()
                    .First(m => m.Name == "Ascending" && m.IsGenericMethod)
                    .MakeGenericMethod(propertyType);

                var descendingMethod = typeof(KeysetPaginationBuilder<Chat>)
                    .GetMethods()
                    .First(m => m.Name == "Descending" && m.IsGenericMethod)
                    .MakeGenericMethod(propertyType);

                var propertyId = Expression.Lambda<Func<Chat, int>>(
                    Expression.PropertyOrField(parameter, nameof(Chat.Id)),
                    parameter
                );

                if (reverse == true)
                {
                    descendingMethod.Invoke(builder, new object[] { lambda });
                    builder.Descending(propertyId);
                }
                else
                {
                    ascendingMethod.Invoke(builder, new object[] { lambda });
                    builder.Ascending(propertyId);
                }
            };


        }
    }
}
