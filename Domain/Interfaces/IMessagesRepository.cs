using Domain.Models;
using Domain.Records;

namespace Domain.Interfaces
{
    public interface IMessagesRepository
    {
        Task<KeysetPaginationAfterResult<Message>> GetMessagesKeysetPaginationAsync(int chatId, string? after, string? propName, int? limit, int? Id, bool? reverse);

        Task<Message> DeleteMessage(int messageId);
    }
}
