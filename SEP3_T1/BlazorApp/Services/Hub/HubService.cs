using System.Text.Json;
using Entities.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Services.Hub; 

public class HubService {
    
    private const string Path = "https://localhost:7777/chat";

    private HubConnection? _hubConnection;

    public async Task<HubConnection> GetHubConnection() {
        _hubConnection ??=  new HubConnectionBuilder().WithUrl(Path).Build();
        _hubConnection.On<String>("Brodcast", ReceiveMessage);
        return _hubConnection;
    }
    
    private void ReceiveMessage(string messageInJson) {

        //Convert Message from Json
        Message message = JsonSerializer.Deserialize<Message>(messageInJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
    }
}