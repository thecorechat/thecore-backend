
using Application.Models;
using Application.ModelsDTO;

namespace Application.Interfaces;

public interface IChatsService
{


    Task<ChatResponseDTO> CreateChatAsync(ChatCreateDTO dto);
    Task<IEnumerable<ChatResponseDTO>> GetAvailableChatsAsync();
    Task<ChatResponseDTO> GetChatAsync(int chatId, int includeMessages = 100);
    Task<ChatResponseDTO> DeleteChatAsync(int chatId);
}
