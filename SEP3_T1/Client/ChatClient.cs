using System.Text;
using System.Text.Json;
using Contracts.Services;
using Entities.Address;
using Entities.Model;

namespace Client; 

public class ChatClient : IChatService {
    public async Task<ICollection<Chat>> GetAllChats() {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_CHAT);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Chat> chats = JsonSerializer.Deserialize<ICollection<Chat>>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return chats;
    }

    public async Task SendMessage(Message message) {
        throw new NotImplementedException();
    }

    public async Task<Chat> GetOrCreateChat(Guid userOne, Guid UserTwo) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_CHAT + $"/user/{userOne}/{UserTwo}");
   
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Chat chat = JsonSerializer.Deserialize<Chat>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return chat;
    }

    public async Task<Chat> AddGroupChat(Chat chat) {
        using HttpClient client = new();
        string chatToJson = JsonSerializer.Serialize(chat);
        StringContent content = new(chatToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(Address.ENDPOINT_CHAT, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
        
        Chat returned = JsonSerializer.Deserialize<Chat>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return returned;
    }


    public async Task<Chat> GetChat(Guid CUI) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_CHAT + $"/chat/{CUI}");
   
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Chat chat = JsonSerializer.Deserialize<Chat>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return chat;
    }

    public async Task<ICollection<Chat>> GetAllChatsByUser(Guid RUIUser) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_CHAT + $"/user/{RUIUser}/chat");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Chat> chats = JsonSerializer.Deserialize<ICollection<Chat>>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return chats;
    }

    public async Task<Chat> UpdateChat(Chat chat) {
        using HttpClient client = new();
        string chatToJson = JsonSerializer.Serialize(chat);
        StringContent content = new(chatToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PatchAsync(Address.ENDPOINT_CHAT, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response.Version);

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        Chat returned = JsonSerializer.Deserialize<Chat>(responseContent, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return returned;
    }

    public async Task SetAsReadMessages(Guid myUserRui, Chat chat) {
        foreach (var message in chat!.Messages.Where(m=>!m.Header.CreatedBy.RUI.Equals(myUserRui))) {
            message.Read = true;
        }
        if (chat!.Messages.Any()) {
            await UpdateChat(chat);
        }
    }
}