using System.Text.Json;
using Entities.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Services.Hub; 

public class HubService {
    
    private const string Path = "https://localhost:7777/chat";
    
    private HubConnection? hubConnection;

    public async Task<HubConnection> GetHubConnection() {

        try {
            await hubConnection.StartAsync();
        }
        catch (Exception e) {
            hubConnection = new HubConnectionBuilder().WithUrl(Path).WithAutomaticReconnect().Build();
        }
        
        if (hubConnection.State == HubConnectionState.Disconnected) {
            await hubConnection.StartAsync();
        }

        // Console.WriteLine("desde el hubservice: " + hubConnection.State);
        //     hubConnection.On<string>("Broadcast", SendMessage);
        //     await hubConnection.StartAsync();
            return hubConnection;
    }

//     public event Func<Task> Notify;
//     
//     public Message? Message { get; set; }
//     
//     
//     private void SendMessage(string messageInJson) {
//
//         //Convert Message from Json
//         Message message = JsonSerializer.Deserialize<Message>(messageInJson, new JsonSerializerOptions
//         {
//             PropertyNameCaseInsensitive = true
//         })!;
//
//         Message = message;
//         Notify.Invoke();
//     }
}