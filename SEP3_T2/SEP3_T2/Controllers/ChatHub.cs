using Microsoft.AspNetCore.SignalR;

namespace SEP3_T2.Controllers;

public class ChatHub : Hub {
    public const string HubUrl = "/chat";

    public async Task Broadcast(string message) {
        await Clients.All.SendAsync("Broadcast", message);
        Console.WriteLine(message);
    }

    public async Task NewConnection(string user) {
        await Clients.All.SendAsync("NewConnection", user);
        Console.WriteLine(user);
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