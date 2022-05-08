using System.Text.Json;
using Entities.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Services.Hub; 

public class HubService {
    public HubConnection? HubConnection;

    public event Func<Task> Notify;
    public Message? Message { get; set; }

    public HubService() { }

    public HubConnection GetHubConnection() {
        if (HubConnection == null) {
             HubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7777/chat").Build();
             HubConnection.On<string>("Broadcast", SendMessage);
        }

        return HubConnection;
    }
    
    private void SendMessage(string messageInJson) {

        //Convert Message from Json
        Message message = JsonSerializer.Deserialize<Message>(messageInJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        Message = message;
        Notify.Invoke();
    }
}