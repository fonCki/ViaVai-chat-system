using System.Text.Json;
using Contracts.Services;
using Contracts.Services.Hub;
using Entities.Address;
using Entities.Model;

namespace Client; 

public class MessageClient : IMessageService {
    public Task SendMessage(Message message) {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Message>> GetAllMessagesFromChat(Guid CUI) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_MESSAGE + $"/Chat/{CUI}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        ICollection<Message> messages = JsonSerializer.Deserialize<ICollection<Message>>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return messages;
    }
}