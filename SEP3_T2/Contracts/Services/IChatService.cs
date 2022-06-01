using Entities.Model;

namespace Contracts.Services; 

public interface IChatService {
    public Task<ICollection<Chat>> GetAllChats();
    public Task<Chat> GetOrCreateChat(Guid userOne, Guid userTwo);
    public Task<Chat> GetChat(Guid CUI);
    public Task<ICollection<Chat>> GetAllChatsByUser(Guid RUIUser);
    public Task<Chat> UpdateChat(Chat chat);
    public Task<ICollection<Guid>> GetAllUsersFromChat(Guid CUI);
    public Task<Chat> CreateGroupChat(Chat chat);
}