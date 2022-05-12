using System.Text.Json;
using Contracts.Services;
using Entities.Model;
using Microsoft.AspNetCore.SignalR;
using RESTClient;

namespace SEP3_T2.Controllers;

public class ChatHub : Hub {
    
    public const string HubUrl = "api/chatHub";

    private IMessageService MessageService;

    public ChatHub(IMessageService messageService) {
        MessageService = messageService;
    }

    public async Task Broadcast(string message) {
        //  await Clients.All.SendAsync("Broadcast", message);
        //  // await MessageHTTPClient.AddMessage(message);
        // Console.WriteLine(message);
    }


    public async Task SendMessage(string messageAsJson) {
        Message message = JsonSerializer.Deserialize<Message>(messageAsJson, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        await MessageService.SaveMessage(message);
        await Clients.All.SendAsync("NotifyAll", messageAsJson);
    }
    
    
    public async Task NewMessageNotification() {
        await Clients.All.SendAsync("NewMessage");
        // await MessageHTTPClient.AddMessage(message);
    }
    
    public override Task OnConnectedAsync()
    {
        Clients.All.SendAsync("NewLogin", "Hay un nuevo usuario");
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}