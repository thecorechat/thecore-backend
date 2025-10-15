
using Application.Models;
using Application.ModelsDTO;

namespace Application.Interfaces
{
    public interface IChatsManager
    {
        Task<IEnumerable<ChatResponseDTO>> ChatsContainsTheUser(int userId);
        Task<GroupIdentifier> GetGroupIdentifierByGroupIdAsync(int chatId);
    }
}
