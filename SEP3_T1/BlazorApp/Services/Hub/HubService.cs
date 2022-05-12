using System.Runtime.CompilerServices;
using System.Text.Json;
using Entities.Address;
using Entities.Model;
using Microsoft.AspNetCore.SignalR.Client;


namespace BlazorApp.Services.Hub; 

public class HubService {
    
    public HubConnection? HubConnection { get; private set; }
    
    public Message Message { get; set; }
    // public event Func<Task> NotifyNewMessage;
    
    public Action<string> NotifyNewLogin;
    
    public Action<Message>? NotifyAllNewMessage;
    
    public async Task InitHubConnection() {
        
        try {         
            HubConnection ??=  new HubConnectionBuilder().WithUrl(Address.ENDPOINT_HUB).Build();
            // _hubConnection.On<string>("Broadcast", ReceiveMessage);
            // HubConnection.On("NewMessage", () =>  NotifyNewMessage.Invoke() );
            HubConnection.On<string>("NewLogin", NewLogin);
            HubConnection.On<string>("NotifyAll", NotifyAll); }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }

    }

    private async Task NotifyAll(string messajeAsJson) {
        Message message = JsonSerializer.Deserialize<Message>(messajeAsJson, new JsonSerializerOptions {
                 PropertyNameCaseInsensitive = true
             })!;
        
        NotifyAllNewMessage?.Invoke(message);
    }
    
    private void NewLogin(string userInJson) {
        Console.WriteLine(userInJson);
        // User user = JsonSerializer.Deserialize<User>(userInJson, new JsonSerializerOptions {
        //     PropertyNameCaseInsensitive = true
        // })!;
        NotifyNewLogin.Invoke(userInJson);
    }



}
