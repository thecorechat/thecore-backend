
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class ChatsRepository : IChatsCRUDRepository
    {
        private SchoolChatContext ChatContext { get; init; }

        public ChatsRepository(SchoolChatContext schoolChatContext)
        {
            ChatContext = schoolChatContext;
        }

        public async Task<Chat> CreateChatAsync(Chat dto)
        {
            var result = ChatContext.Chats.Add(dto).Entity;
            await ChatContext.SaveChangesAsync();
            return result;
        }

        public async Task<Chat> DeleteChatAsync(int chatId)
        {
            var chat = await ChatContext.Chats.SingleAsync(c => c.Id == chatId);
            var result = ChatContext.Chats.Remove(chat).Entity;
            await ChatContext.SaveChangesAsync();
            return result;
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            var chat = await ChatContext.Chats.SingleAsync(c => c.Id == chatId);
            return chat;
        }

        public async Task<Chat> UpdateChatAsync(int chatId, Chat dto)
        {
            var chat = await ChatContext.Chats.SingleAsync(c => c.Id == chatId);
            ChatContext.Entry(chat).CurrentValues.SetValues(dto);
            var result = ChatContext.Chats.Update(chat).Entity;
            await ChatContext.SaveChangesAsync();
            return result;
        }
    }
}
