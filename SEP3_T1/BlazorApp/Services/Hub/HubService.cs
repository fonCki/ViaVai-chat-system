using System.Runtime.CompilerServices;
using System.Text.Json;
using Entities.Model;
using Microsoft.AspNetCore.SignalR.Client;


namespace BlazorApp.Services.Hub; 

public class HubService {
    
    private const string Path = "https://localhost:7777/chat";

    public HubConnection? HubConnection { get; private set; }
    
    public Message Message { get; set; }
    public event Func<Task> NotifyNewMessage;
    
    public Action<string> NotifyNewLogin;
    
    public async Task InitHubConnection() {
        HubConnection ??=  new HubConnectionBuilder().WithUrl(Path).Build();
        // _hubConnection.On<string>("Broadcast", ReceiveMessage);
        HubConnection.On("NewMessage", () =>  NotifyNewMessage.Invoke() );
        HubConnection.On<string>("NewLogin", NewLogin);
    }

    private void NewLogin(string userInJson) {
        Console.WriteLine(userInJson);
        // User user = JsonSerializer.Deserialize<User>(userInJson, new JsonSerializerOptions {
        //     PropertyNameCaseInsensitive = true
        // })!;
        NotifyNewLogin.Invoke(userInJson);
    }

    
}
