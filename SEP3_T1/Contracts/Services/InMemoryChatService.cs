using System.Text.Json;
using Entities.Model;

namespace Contracts.Services; 

public class InMemoryChatService : IChatService{
    
    private string chatPath = "/Users/alfonsoridao/Library/CloudStorage/OneDrive-ViaUC/Via University/Semester 3/SEP3/Sep3_Project/SEP3_T1/Contracts/Services/JsonFiles/chats.json";

    private ICollection<Chat> _chats;

    public InMemoryChatService() {
        LoadOrCreate();
    }

    public async Task<ICollection<Chat>> GetAllChats() {
        if (_chats == null) {
            LoadOrCreate();
        }

        return _chats;
    }

    public async Task SendMessage(Message message) {
        if (_chats == null) {
            LoadOrCreate();
        }

        Console.WriteLine(message.Body + "este es el mesaje");
        //TODO this works only with 2 Persons chat, you must know that
        Chat chat = await GetOrCreateChat(message.Header.CreatedBy, message.Header.Recipient);
        
        chat.Messages.Add(message);
        chat.Messages = chat.Messages.OrderByDescending(m => m.Header.Created).ToList();
        SaveChangesAsync();
    }

    public async Task<Chat> GetOrCreateChat(User userOne, User userTwo) {
        if (_chats == null) {
            LoadOrCreate();
        }

        //Filter by single chats
        var singleChats= _chats.Where(c => (c.Subscribers.Count == 2));
        
        Console.WriteLine(singleChats.Count());
        
        //Return a available chat between this 2 users
        var chat = singleChats.Where(c => c.Subscribers.Any(u => u.RUI.Equals(userOne.RUI))).Where(c => c.Subscribers.Any(u => u.RUI.Equals(userTwo.RUI))).FirstOrDefault();
        
        if (chat != null)
            Console.WriteLine("Este es el chat que encontre: " + chat.CID); //TODO to eliminate
        
        if (chat == null) {
            chat = new Chat();
            chat.Subscribers.Add(userOne);
            chat.Subscribers.Add(userTwo);
            _chats.Add(chat);
        }
        return chat;
    }
    

    public async Task SaveChangesAsync() {
        var chatAsJson = JsonSerializer.Serialize(_chats, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(chatPath, chatAsJson);
        _chats = null;
    }
    
    private async Task LoadOrCreate() {
        if (File.Exists(chatPath)) {
            var chatAsJson = File.ReadAllText(chatPath);
            _chats = JsonSerializer.Deserialize<ICollection<Chat>>(chatAsJson)!;
        }
        else {
            _chats = new List<Chat>();
            Task.FromResult(SaveChangesAsync());
        }
        
    }
}