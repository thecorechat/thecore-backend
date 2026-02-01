using Domain.Models;
using Domain.Records;
using System.Reflection;

namespace Domain.Interfaces
{
    public interface IMessagesRepository
    {

        Task<Message> CreateMessageAsync(Message message);
        Task<Message?> GetMessageByIdAsync(int messageId);
        Task<KeysetPaginationAfterResult<Message>> GetMessagesKeysetPaginationAsync(string? after, string propName, int limit, bool IsDescending);
        Task<Message> UpdateMessageAsync(int messageId, Message message);
        Task<Message> DeleteMessageAsync(int messageId);

    }
}
