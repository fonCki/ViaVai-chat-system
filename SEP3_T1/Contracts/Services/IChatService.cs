using Entities.Model;

namespace Contracts.Services; 

public interface IChatService {
    public Task<ICollection<Chat>> GetAllChats(); // TODO this is just for test
    public Task SendMessage(Message message);
    public Task<Chat> GetOrCreateChat(User userOne, User userTwo);
    public Task<Chat> GetOrCreateChat(Guid CUI);
    public Task<ICollection<Chat>> GetAllChatsByUser(User user);
    public Task<Chat> UpdateChat(Chat chat);
    public Task MarkAsRead(Guid CUI, User myself);
}