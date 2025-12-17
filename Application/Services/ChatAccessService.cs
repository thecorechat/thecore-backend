
using Application.Interfaces;
using Application.Models;
using Application.ModelsDTO;
using Domain.Interfaces;
using Domain.Models;
using Domain.Records;
using Mapster;
using System.Reflection;
namespace Application.Services;

public class ChatAccessService : IChatAccessService
{
    private IChatsQueryRepository QueryRepository { get; init; }
    private IChatsCRUDRepository CRUDRepository { get; init; }

    public ChatAccessService(IChatsQueryRepository queryRepository, IChatsCRUDRepository crudRepository)
    {
        QueryRepository = queryRepository;
        CRUDRepository = crudRepository;
    }

    public async Task<KeysetPaginationAfterResult<ChatResponseDTO>> GetAvailableChatsAsync(string? after = null, string? propName = null, int? limit = null, bool? reverse = null)
    {
        string sortByProp = propName ?? nameof(Chat.Users);
        limit = limit > 1000 ? 1000 : 100;
        reverse ??= false;
        PropertyInfo? propInf = typeof(Message).GetProperties().FirstOrDefault(p => p.Name == propName)
            ?? throw new ArgumentException($"Property '{propName}' does not exist on Message.");

        var ksr = await QueryRepository.GetChatKeysetPaginationAsync(after, propInf, (int)limit, (bool)reverse);
        return ksr.Adapt<KeysetPaginationAfterResult<ChatResponseDTO>>();
    }

    public async Task<IEnumerable<ChatResponseDTO>> ChatsContainsTheUser(int userId)
    {
        var chats = await QueryRepository.ChatsContainsTheUser(userId);
        return chats.Adapt<List<ChatResponseDTO>>();
    }

    public async Task<GroupIdentifier> GetGroupIdentifierByGroupIdAsync(int chatId)
    {
        var chat = await CRUDRepository.GetChatByIdAsync(chatId);
        return new GroupIdentifier(chat.Title, chat.Id);
    }

}
