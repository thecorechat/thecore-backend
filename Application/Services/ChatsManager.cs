using Application.Interfaces;
using Application.ModelsDTO;
using Domain.Interfaces;
using Domain.Models;
using Mapster;

namespace Application.Services
{
    public class ChatsManager : IChatsService
    {
        private IChatsCRUDRepository CRUDRepository { get; init; }

        public ChatsManager(IChatsCRUDRepository chatsRepository)
        {
            CRUDRepository = chatsRepository;
        }

        public async Task<ChatResponseDTO> CreateChatAsync(ChatCreateDTO dto)
        {
            var chat = dto.Adapt<Chat>();
            var result = await CRUDRepository.CreateChatAsync(chat);
            return result.Adapt<ChatResponseDTO>();
        }


        public async Task<ChatResponseDTO> GetChatAsync(int chatId)
        {
            var chat = await CRUDRepository.GetChatByIdAsync(chatId);
            return chat.Adapt<ChatResponseDTO>();
        }
        public async Task<ChatResponseDTO> UpdateChatAsync(int chatId, ChatUpdateDTO dto)
        {
            var chat = dto.Adapt<Chat>();
            var result = await CRUDRepository.UpdateChatAsync(chatId, chat);
            return result.Adapt<ChatResponseDTO>();
        }

        public async Task<ChatResponseDTO> DeleteChatAsync(int chatId)
        {
            var chat = await CRUDRepository.DeleteChatAsync(chatId);
            return chat.Adapt<ChatResponseDTO>();
        }
    }
}
