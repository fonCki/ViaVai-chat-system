using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Contracts.DAO;
using Entities.Address;
using Entities.Model;

namespace RESTClient; 

public class MessageDAO : IMessageDao {
    
    public async Task<Message> AddMessage(Message message) {
        using HttpClient client = new();
        string messageToJson = JsonSerializer.Serialize(message);
        StringContent content = new(messageToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(Address.ENDPOINT_MESSAGE, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        Message returned = JsonSerializer.Deserialize<Message>(responseContent, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;

        return returned;
    }

    public async Task<Message> GetMessage(Guid MID) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_MESSAGE + $"/{MID}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Message message = JsonSerializer.Deserialize<Message>(content, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return message;
    }

    public async Task<ICollection<Message>> GetAllMessage() {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_MESSAGE);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        if (string.IsNullOrWhiteSpace(content)) {
            return new List<Message>();
        }
        ICollection<Message> messages = JsonSerializer.Deserialize<ICollection<Message>>(content, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return messages;
    }

    public async Task DeleteMessage(Guid MID) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.DeleteAsync(Address.ENDPOINT_MESSAGE + $"/{MID}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}");
        }
    }

    public async Task<Message> UpdateMessage(Message message) {
        using HttpClient client = new();
        string messageToJson = JsonSerializer.Serialize(message);
        StringContent content = new(messageToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PatchAsync(Address.ENDPOINT_MESSAGE, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        Message returned = JsonSerializer.Deserialize<Message>(responseContent, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return returned;
    }
}