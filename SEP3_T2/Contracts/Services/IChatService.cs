using Entities.Model;

namespace Contracts.Services; 

public interface IChatService {
    public Task<ICollection<Chat>> GetAllChats(); // TODO this is just for test
    public Task SaveMessage(Message message);
    public Task<Chat> GetOrCreateChat(Guid userOne, Guid UserTwo);
    public Task<Chat> GetChat(Guid CUI);
    public Task<ICollection<Chat>> GetAllChatsByUser(Guid RUIUser);
    public Task<Chat> UpdateChat(Chat chat);
}