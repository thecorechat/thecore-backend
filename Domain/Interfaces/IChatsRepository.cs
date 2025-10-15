
using Domain.Models;
using Domain.Records;

namespace Domain.Interfaces
{
    public interface IChatsRepository
    {

        Task<IEnumerable<Chat>> ChatsContainsTheUser(int userId);


        Task<Chat> CreateChatAsync(Chat chatToCreate);
        Task<KeysetPaginationAfterResult<Chat>> GetChatKeysetPaginationAsync(string? after, string? propName, int? limit, int? Id, bool? reverse);
        Task<Chat> GetChatAsync(int chatId, int includeMessages = 100);
        Task<Chat> DeleteChatAsync(int chatId);
    }
}
