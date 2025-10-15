using Application.Interfaces;
using Application.Models;
using Application.ModelsDTO;
using Domain.Interfaces;

namespace Application.Services
{
    public class ChatsService : IChatsService
    {
        private IChatsRepository ChatsRepository { get; init; }

        public ChatsService(IChatsRepository chatsRepository)
        {
            ChatsRepository = chatsRepository;
        }

        public Task<IEnumerable<ChatResponseDTO>> ChatsContainsTheUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<GroupIdentifier> GetGroupIdentifierByGroupIdAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task<ChatResponseDTO> CreateChatAsync(ChatCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatResponseDTO>> GetAvailableChatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ChatResponseDTO> GetChatAsync(int chatId, int includeMessages = 100)
        {
            throw new NotImplementedException();
        }

        public Task<ChatResponseDTO> DeleteChatAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatResponseDTO>> ChatsContainsTheUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
