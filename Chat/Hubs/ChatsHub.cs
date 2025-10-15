using Application.Interfaces;
using Application.Models;
using Application.ModelsDTO;
using ChatApi.Hubs.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApi.Hubs;

public class ChatsHub : Hub, IChatsHub
{

    public IChatsManager ChatsManager{ get; init; }

    public ChatsHub (IChatsManager chatsManager)
    {
        ChatsManager = chatsManager;
    }

    public override async Task OnConnectedAsync()
    {
        if(Context.ConnectionId is null)
        {
            new Exception("Context is null");
        }

        if (int.TryParse(Context?.User?.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value, out int userId) == false)
        {
            new Exception("User id is null or incorrect");
        }

        HashSet<GroupIdentifier> groupsIdentifiersContainsTheUser =
            [.. (await ChatsManager.ChatsContainsTheUser(userId)).Select(g => new GroupIdentifier(g.Title, g.Id))];
        foreach (var groupIdentifier in groupsIdentifiersContainsTheUser)
        {
            await Groups.AddToGroupAsync(Context!.ConnectionId!, groupIdentifier.ToString());
        }

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {


        return base.OnDisconnectedAsync(exception);
    }

    public async Task UpdateMessageAsync(MessageResponseDTO message) 
    {
        GroupIdentifier groupIdentifier = await ChatsManager.GetGroupIdentifierByGroupIdAsync(message.ChatId);
        await Clients.Group(groupIdentifier.ToString()).SendAsync("UpdateMessage", message);
    }

    public async Task SendMessageAsync(MessageResponseDTO message)
    {
        GroupIdentifier groupIdentifier = await ChatsManager.GetGroupIdentifierByGroupIdAsync(message.ChatId);
        await Clients.Group(groupIdentifier.ToString()).SendAsync("ReceiveMessage", message);
    }

    public async Task DeleteMessageAsync(MessageResponseDTO message)
    {
        GroupIdentifier groupIdentifier = await ChatsManager.GetGroupIdentifierByGroupIdAsync(message.ChatId);
        await Clients.Group(groupIdentifier.ToString()).SendAsync("DeleteMessage", message);
    }
}
