
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IChatsCRUDRepository
    {
        Task<Chat> CreateChatAsync(Chat dto);
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<Chat> UpdateChatAsync(int chatId, Chat dto);
        Task<Chat> DeleteChatAsync(int chatId);
    }
}
