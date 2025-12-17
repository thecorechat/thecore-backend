
using Domain.Models;
using Domain.Records;
using System.Reflection;

namespace Domain.Interfaces
{
    public interface IChatsQueryRepository
    {
        Task<KeysetPaginationAfterResult<Chat>> GetChatKeysetPaginationAsync(string? after, PropertyInfo propInf, int limit, bool reverse);
        Task<KeysetPaginationAfterResult<Chat>> GetChatKeysetPaginationAsync(string? after, string propName, int limit, bool reverse);

        Task<IEnumerable<Chat>> ChatsContainsTheUser(int userId);

    }
}
