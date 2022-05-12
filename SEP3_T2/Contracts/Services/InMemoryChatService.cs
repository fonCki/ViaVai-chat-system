using System.Text.Json;
using Entities.Model;

namespace Contracts.Services; 

public class InMemoryChatService : IChatService{
    
    private string chatPath = "/Users/alfonsoridao/Library/CloudStorage/OneDrive-ViaUC/Via University/Semester 3/SEP3/Sep3_Project/SEP3_T2/Contracts/Services/JsonFiles/chats.json";

    private ICollection<Chat> _chats;

    private IUserService UserService;

    public InMemoryChatService(IUserService userService) {
        UserService = userService;
        LoadOrCreate();
    }

    public async Task<ICollection<Chat>> GetAllChats() {
        if (_chats == null) {
            await LoadOrCreate();
        }

        return _chats;
    }

    public async Task SaveMessage(Message message) {
        if (_chats == null) {
            await LoadOrCreate();
        }


        Chat? chat = await GetChat(message.Header.CUIRecipient);

        if (chat == null) {
            throw new Exception("Internal Error, Chat could not be found");
        }
        chat.Messages.Add(message);
        chat.Messages = chat.Messages.OrderByDescending(m => m.Header.Created).ToList();
         await SaveChangesAsync();
    }

    public async Task<Chat> GetChat(Guid CUI) {
        if (_chats == null) {
            await LoadOrCreate();
        }
        return _chats.FirstOrDefault(c => c.CID.Equals(CUI))!;
    }

    public async Task<Chat> GetOrCreateChat(Guid userOne, Guid userTwo) {
        if (_chats == null) {
            await LoadOrCreate();
        }

        if (userOne == null || userTwo == null) {
            throw new Exception("User can't be null mate! fix this");
        }

        if (userOne.Equals(userTwo)) {
            throw new Exception("Can't create a chat with the same person");
        }
        
        

        //Filter by single chats
        var singleChats= _chats.Where(c => (c.Subscribers.Count == 2));
        
        
        //Return a available chat between this 2 users
        var chat = singleChats.Where(c => c.Subscribers.Any(u => u.RUI.Equals(userOne))).Where(c => c.Subscribers.Any(u => u.RUI.Equals(userTwo))).FirstOrDefault();
        
        
        if (chat == null) {
            chat = new Chat();
            chat.Subscribers.Add(await UserService.GetUserAsyncByRUI(userOne));
            chat.Subscribers.Add(await UserService.GetUserAsyncByRUI(userTwo));
            _chats.Add(chat);
        }

        await SaveChangesAsync();
        return chat;
    }

    public async Task<ICollection<Chat>> GetAllChatsByUser(Guid RUI) {
        if (_chats == null) {
            await LoadOrCreate();
        }

        ICollection<Chat> chats = new List<Chat>();
        chats = _chats.Where(c => c.Subscribers.Any(u => u.RUI.Equals(RUI))).ToList();
        return chats;
    }

    public async Task<Chat> UpdateChat(Chat chat) {
        if (_chats == null) {
            await LoadOrCreate();
        }

        Chat chatToUpdate = await GetChat(chat.CID);
        chatToUpdate.Messages = chat.Messages;
        chatToUpdate.Subscribers = chat.Subscribers;
        await SaveChangesAsync();
        return chatToUpdate;
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
            await Task.FromResult(SaveChangesAsync());
        }
        
    }
}