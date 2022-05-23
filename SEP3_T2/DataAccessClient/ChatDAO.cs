using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Contracts.DAO;
using Entities.Address;
using Entities.Model;

namespace RESTClient; 

public class ChatDAO : IChatDao {
    
    public async Task<Chat> AddChat(Chat chat) {
        using HttpClient client = new();
        string chatToJson = JsonSerializer.Serialize(chat);
        StringContent content = new(chatToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(Address.ENDPOINT_CHAT, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        Chat returned = JsonSerializer.Deserialize<Chat>(responseContent, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;

        return returned;
    }

    public async Task<Chat> GetChat(Guid CID) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_CHAT + $"/{CID}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Chat chat = JsonSerializer.Deserialize<Chat>(content, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return chat;
    }

    public async Task<ICollection<Chat>> GetAllChat() {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_CHAT);
        string content = await response.Content.ReadAsStringAsync();
        

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        if (string.IsNullOrEmpty(content) || content.Equals("[]")) { //IF IS EMPTY
            return new List<Chat>();
        }

        ICollection<Chat> chats = JsonSerializer.Deserialize<ICollection<Chat>>(content, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return chats;
    }

    public async Task<ICollection<Chat>> GetChatsFromParticularUser(Guid RUI) { //TODO IMPLEMENT THIS
        ICollection<Chat> fullCollection = await GetAllChat();
        ICollection<Chat> c = fullCollection.Where(c => c.Subscribers.Any(u => u.RUI.Equals(RUI))).ToList();
        return c;
    }

    public async Task DeleteChat(Guid CID) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.DeleteAsync(Address.ENDPOINT_CHAT + $"/{CID}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}");
        }
    }

    public async Task<Chat> UpdateChat(Chat chat) {
        using HttpClient client = new();
        string chatToJson = JsonSerializer.Serialize(chat);
        StringContent content = new(chatToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PatchAsync(Address.ENDPOINT_CHAT, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        Chat returned = JsonSerializer.Deserialize<Chat>(responseContent, new JsonSerializerOptions {
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