using Application.ModelsDTO;

namespace ChatApi.Hubs.Interfaces
{
    public interface IChatsHub
    {
        Task UpdateMessageAsync(MessageResponseDTO message);

        Task SendMessageAsync(MessageResponseDTO message);

        Task DeleteMessageAsync(MessageResponseDTO message);
    }
}
