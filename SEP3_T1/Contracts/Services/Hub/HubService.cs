using System.Runtime.CompilerServices;
using System.Text.Json;
using Entities.Address;
using Entities.Model;
using Microsoft.AspNetCore.SignalR.Client;


namespace Contracts.Services.Hub;

public class HubService {
    
    public HubConnection? HubConnection { get; private set; }

    public Action<Guid>? NotifyNewLogin;

    public Action<Guid>? NotifyLogOff;
    
    public Action<Guid>? NotifyStatusChanged;

    public Action<Message>? NotifyAllNewMessage;
    
    public async Task InitHubConnection() {
        
        try {         
            HubConnection ??=  new HubConnectionBuilder().WithUrl(Address.ENDPOINT_HUB).Build();
            HubConnection.On<Guid>("NewUser", (guid => NotifyNewLogin?.Invoke(guid)));
            HubConnection.On<Guid>("DisconnectUser", (guid => NotifyLogOff?.Invoke(guid)));
            HubConnection.On<Guid>("StatusChanged", (guid => NotifyStatusChanged?.Invoke(guid)));
            HubConnection.On<string>("NewMessage", NewMessage); }
        catch (Exception e) {
            Console.WriteLine(e);
        }

    }

    private async Task NewMessage(string messajeAsJson) {
        Message message = JsonSerializer.Deserialize<Message>(messajeAsJson, new JsonSerializerOptions {
                 PropertyNameCaseInsensitive = true
             })!;
        
        NotifyAllNewMessage?.Invoke(message);
    }
    
}
