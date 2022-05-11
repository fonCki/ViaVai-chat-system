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
            LoadOrCreate();
        }

        return _chats;
    }

    public async Task SaveMessage(Message message) {
        if (_chats == null) {
            LoadOrCreate();
        }


        Chat? chat = await GetChat(message.Header.CUIRecipient);

        if (chat == null) {
            throw new Exception("Internal Error, Chat could not be found");
        }
        chat.Messages.Add(message);
        chat.Messages = chat.Messages.OrderByDescending(m => m.Header.Created).ToList();
        SaveChangesAsync();
    }

    public async Task<Chat> GetChat(Guid CUI) {
        return _chats.FirstOrDefault(c => c.CID.Equals(CUI));
    }

    public async Task<Chat> GetOrCreateChat(Guid userOne, Guid userTwo) {
        if (_chats == null) {
            LoadOrCreate();
        }

        if (userOne.Equals(userTwo)) {
            throw new Exception("Can't create a chat with the same person");
        }

        //Filter by single chats
        var singleChats= _chats.Where(c => (c.Subscribers.Count == 2));
        
        Console.WriteLine(singleChats.Count());
        
        //Return a available chat between this 2 users
        var chat = singleChats.Where(c => c.Subscribers.Any(u => u.RUI.Equals(userOne))).Where(c => c.Subscribers.Any(u => u.RUI.Equals(userTwo))).FirstOrDefault();
        
        if (chat != null)
            Console.WriteLine("Este es el chat que encontre: " + chat.CID); //TODO to eliminate
        
        if (chat == null) {
            chat = new Chat();
            chat.Subscribers.Add(await UserService.GetUserAsyncByRUI(userOne));
            chat.Subscribers.Add(await UserService.GetUserAsyncByRUI(userTwo));
            _chats.Add(chat);
        }

        Console.WriteLine("Me gustaria imprimir este chat: " + chat.CID);
        return chat;
    }

    public async Task<ICollection<Chat>> GetAllChatsByUser(Guid RUI) {
        if (_chats == null) {
            LoadOrCreate();
        }
        ICollection<Chat>? chats = _chats.Where(c => c.Subscribers.Any(u => u.RUI.Equals(RUI))).ToList() as ICollection<Chat>;

        return chats;
    }

    public async Task<Chat> UpdateChat(Chat chat) {
        if (_chats == null) {
            LoadOrCreate();
        }

        Chat chatToUpdate = await GetChat(chat.CID);
        chatToUpdate.Messages = chat.Messages;
        chatToUpdate.Subscribers = chat.Subscribers;
        SaveChangesAsync();
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
            Task.FromResult(SaveChangesAsync());
        }
        
    }
}