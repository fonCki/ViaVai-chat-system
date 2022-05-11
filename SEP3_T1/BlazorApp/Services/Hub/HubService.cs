using System.Runtime.CompilerServices;
using System.Text.Json;
using Entities.Address;
using Entities.Model;
using Microsoft.AspNetCore.SignalR.Client;


namespace BlazorApp.Services.Hub; 

public class HubService {
    
    public HubConnection? HubConnection { get; private set; }
    
    public Message Message { get; set; }
    public event Func<Task> NotifyNewMessage;
    
    public Action<string> NotifyNewLogin;
    
    public async Task InitHubConnection() {
        HubConnection ??=  new HubConnectionBuilder().WithUrl(Address.ENDPOINT_HUB).Build();
        // _hubConnection.On<string>("Broadcast", ReceiveMessage);
        HubConnection.On("NewMessage", () =>  NotifyNewMessage.Invoke() );
        HubConnection.On<string>("NewLogin", NewLogin);
        HubConnection.On("NotifyAll", () => NotifyNewMessage.Invoke());
    }

    private void NewLogin(string userInJson) {
        Console.WriteLine(userInJson);
        // User user = JsonSerializer.Deserialize<User>(userInJson, new JsonSerializerOptions {
        //     PropertyNameCaseInsensitive = true
        // })!;
        NotifyNewLogin.Invoke(userInJson);
    }



}
