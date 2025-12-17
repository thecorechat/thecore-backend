
using Application.Interfaces;
using Application.ModelsDTO;
using Domain.Interfaces;
using Domain.Models;
using Domain.Records;
using Mapster;
using System.Reflection;

namespace Application.Services
{
    internal class MessagesService : IMessagesService
    {
        private IMessagesRepository MessagesRepository { get; init; }

        public MessagesService(IMessagesRepository messagesRepository)
        {
            MessagesRepository = messagesRepository;
        }

        public async Task<MessageResponseDTO> CreateMessageAsync(MessageCreateDTO dto)
        {
            var message = dto.Adapt<Message>();
            Message result = await MessagesRepository.CreateMessageAsync(message);
            return result.Adapt<MessageResponseDTO>();
        }

        public async Task<MessageResponseDTO> DeleteMessageAsync(int messageId)
        {
            return (await MessagesRepository.DeleteMessageAsync(messageId))
                .Adapt<MessageResponseDTO>();
        }

        public async Task<KeysetPaginationAfterResult<MessageResponseDTO>> GetMessagesKeysetPaginationAsync(int chatId, string? after = null, string? propName = null, int? limit = null, bool? reverse = null)
        {
            string sortByProp = propName ?? nameof(Message.CreatedAt);
            limit = limit > 1000 ? 1000 : 100;
            reverse ??= false;
            PropertyInfo? propInf = typeof(Message).GetProperties().FirstOrDefault(p => p.Name == propName)
                ?? throw new ArgumentException($"Property '{propName}' does not exist on Message.");

            var result = await MessagesRepository.GetMessagesKeysetPaginationAsync(chatId, after, propInf, (int)limit, (bool)reverse);
            return result.Adapt<KeysetPaginationAfterResult<MessageResponseDTO>>();
        }

        public async Task<MessageResponseDTO> UpdateMessageAsync(int messageId, MessageUpdateDTO dto)
        {
            var message = dto.Adapt<Message>();
            var result = await MessagesRepository.UpdateMessageAsync(messageId, message);
            return result.Adapt<MessageResponseDTO>();
        }
    }
}
