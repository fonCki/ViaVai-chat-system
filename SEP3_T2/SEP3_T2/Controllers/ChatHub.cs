using Microsoft.AspNetCore.SignalR;
using RESTClient;

namespace SEP3_T2.Controllers;

public class ChatHub : Hub {
    public const string HubUrl = "/chat";

    public async Task Broadcast(string message) {
        await Clients.All.SendAsync("Broadcast", message);
       // await MessageHTTPClient.AddMessage(message);
        Console.WriteLine(message);
    }

    public async Task SendMessage(string message) {
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}