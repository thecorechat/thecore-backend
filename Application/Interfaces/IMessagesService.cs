using Application.ModelsDTO;
using Domain.Models;
using Domain.Records;

namespace Application.Interfaces
{
    public interface IMessagesService
    {
        Task<MessageResponseDTO> CreateMessageAsync(MessageCreateDTO dto);

        /// <summary>
        /// Getting <see cref="KeysetPaginationAfterResult{T}"/> (cursor pagination) of the chat (by <paramref name="chatId"/>)
        /// </summary>
        /// <param name="chatId">specifies chat from where will be recieved a pagination</param>
        /// <param name="after">After row</param>
        /// <param name="propName">Sort by prop name(default by time)</param>
        /// <param name="limit">max count of elements in keyset(default 20)</param>
        /// <param name="reverse">reverse the collection? (default <see cref="false"/>/></param>
        /// <returns></returns>
        Task<KeysetPaginationAfterResult<MessageResponseDTO>> GetMessagesKeysetPaginationAsync(int chatId, string? after, string? propName = nameof(Message.CreatedAt), int? limit = 20, bool? reverse = false);

        /// <summary>
        /// Updating existing message by id
        /// </summary>
        /// <param name="messageId">Message id to update</param>
        /// <param name="dto">new values of existing message</param>
        /// <returns></returns>
        Task<MessageResponseDTO> UpdateMessageAsync(int messageId, MessageUpdateDTO dto);

        Task<MessageResponseDTO> DeleteMessageAsync(int messageId);

    }
}
