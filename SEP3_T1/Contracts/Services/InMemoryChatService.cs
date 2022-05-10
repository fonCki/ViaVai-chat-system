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


        Chat? chat = await GetOrCreateChat(message.Header.CUIRecipient);

        if (chat == null) {
            throw new Exception("Internal Error, Chat could not be found");
        }
        chat.Messages.Add(message);
        chat.Messages = chat.Messages.OrderByDescending(m => m.Header.Created).ToList();
        SaveChangesAsync();
    }

    public async Task<Chat> GetOrCreateChat(Guid CUI) {
        return _chats.FirstOrDefault(c => c.CID.Equals(CUI));
    }

    public async Task<Chat> GetOrCreateChat(User userOne, User userTwo) {
        if (_chats == null) {
            LoadOrCreate();
        }

        if (userOne.RUI.Equals(userTwo.RUI)) {
            throw new Exception("Can't create a chat with the same person");
        }

        //Filter by single chats
        var singleChats= _chats.Where(c => (c.Members.Count == 2));
        
  
        
        //Return a available chat between this 2 users
        var chat = singleChats.Where(c => c.Members.Any(u => u.RUI.Equals(userOne.RUI))).Where(c => c.Members.Any(u => u.RUI.Equals(userTwo.RUI))).FirstOrDefault();
        

        
        if (chat == null) {
            chat = new Chat();
            chat.Members.Add(userOne);
            chat.Members.Add(userTwo);
            _chats.Add(chat);
        }
        return chat;
    }

    public async Task<ICollection<Chat>> GetAllChatsByUser(User user) {
        if (_chats == null) {
            LoadOrCreate();
        }
        ICollection<Chat>? chats = _chats.Where(c => c.Members.Any(u => u.RUI.Equals(user.RUI))).ToList() as ICollection<Chat>;

        return chats;
    }

    public async Task<Chat> UpdateChat(Chat chat) {
        throw new NotImplementedException();
    }

    public async Task MarkAsRead(Guid CUI, User myself) {
        Chat chat = await GetOrCreateChat(CUI);
        //Mark as read my messages
        foreach (var message in chat.Messages.Where(m=> !(m.Header.CreatedBy.RUI.Equals(myself.RUI)))) {
            message.Read = true;
        }

        SaveChangesAsync();
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