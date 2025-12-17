using Domain.Models;
using Domain.Records;
using System.Reflection;

namespace Domain.Interfaces
{
    public interface IMessagesRepository
    {

        Task<Message> CreateMessageAsync(Message message);
        Task<Message?> GetMessageByIdAsync(int messageId);
        Task<KeysetPaginationAfterResult<Message>> GetMessagesKeysetPaginationAsync(int chatId, string? after, PropertyInfo propName, int limit, bool reverse);
        Task<Message> UpdateMessageAsync(int messageId, Message message);
        Task<Message> DeleteMessageAsync(int messageId);

    }
}
