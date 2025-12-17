
using Application.Models;
using Application.ModelsDTO;
using Domain.Records;

namespace Application.Interfaces
{
    public interface IChatAccessService
    {
        Task<KeysetPaginationAfterResult<ChatResponseDTO>> GetAvailableChatsAsync(string? after = null, string? propName = null, int? limit = null, bool? reverse = null);
        Task<IEnumerable<ChatResponseDTO>> ChatsContainsTheUser(int userId);
        Task<GroupIdentifier> GetGroupIdentifierByGroupIdAsync(int chatId);
    }
}
