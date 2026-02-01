using Application.Models;
using Application.ModelsDTO;
using Domain.Records;

namespace Application.Services.Interfaces
{
    public interface IChatsAccessService
    {
        Task<KeysetPaginationAfterResult<ChatResponseDTO>> GetAvailableChatsAsync(string? after = null, string? propName = null, int? limit = null, bool? reverse = null);
        Task<IEnumerable<ChatResponseDTO>> ChatsContainsTheUserAsync(int userId);
        Task<GroupIdentifier> GetGroupIdentifierByGroupIdAsync(int chatId);
    }
}
