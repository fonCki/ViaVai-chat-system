using System.Text.Json;
using Application;
using Contracts.Services;
using Entities.Model;
using Microsoft.AspNetCore.SignalR;
using RESTClient;

namespace SEP3_T2.Controllers;

public class ChatHub : Hub {
    
    public const string HubUrl = "api/chatHub";

    // private ControlStatusImp controlStatus = new ControlStatusImp();

    private IMessageService MessageService;
    private IChatService ChatService;
    private IControlStatusUser ControlStatusUser;
    private IUserService UserService;

    public ChatHub(IMessageService messageService, IChatService chatService, IControlStatusUser controlStatusUser, IUserService userService) {
        MessageService = messageService;
        ChatService = chatService;
        ControlStatusUser = controlStatusUser;
        UserService = userService;
    }

    public async Task Welcome(Guid RUIuser) {
        await Groups.AddToGroupAsync(Context.ConnectionId, RUIuser.ToString());
        await ControlStatusUser.InsertOnlineUser(Context.ConnectionId, RUIuser);
        if (!ControlStatusUser.OnlineUsers.Contains(RUIuser)) {
            ControlStatusUser.OnlineUsers.Add(RUIuser);
            await UserService.SetStatus(RUIuser, Status.Online); // In case the user was Connected with client cache
            await Clients.All.SendAsync("NewUser", RUIuser);
        }
    }

    public async Task StatusChanged(Guid RUIUser) {
        await Clients.All.SendAsync("StatusChanged", RUIUser);
    }
    
    public async Task SendMessage(string messageAsJson) {
        Message message = JsonSerializer.Deserialize<Message>(messageAsJson, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        await MessageService.SaveMessage(message);
        Console.WriteLine(message);
        foreach (var user in await ChatService.GetAllUsersFromChat(message.Header.CUIRecipient)) {
            await Clients.Groups(user.ToString()).SendAsync("NewMessage", messageAsJson);
        }

    }
    
    
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"Connected {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public  override async Task OnDisconnectedAsync(Exception? e) {
        Guid RUIuser = await ControlStatusUser.GiveMeUser(Context.ConnectionId); // I Search the user that accept to disconect
        await ControlStatusUser.RemoveConnection(Context.ConnectionId); // I Remove the conection in the dictionary 
        if (!ControlStatusUser.isOnline(RUIuser).Result) { // I wont delete him if he has more open connection.
            ControlStatusUser.OnlineUsers?.Remove(RUIuser); // I Remove the user from the user list
            await UserService.SetStatus(RUIuser, Status.Offline); // In case the user was dissconected without his intention
            await Clients.All.SendAsync("DisconnectUser", RUIuser); // I send the notification that the user is offline
        }

        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}