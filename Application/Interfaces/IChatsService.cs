using Application.ModelsDTO;

namespace Application.Interfaces;

public interface IChatsService
{
    Task<ChatResponseDTO> CreateChatAsync(ChatCreateDTO dto);
    Task<ChatResponseDTO> GetChatAsync(int chatId);
    Task<ChatResponseDTO> UpdateChatAsync(int chatId, ChatUpdateDTO dto);
    Task<ChatResponseDTO> DeleteChatAsync(int chatId);
}
