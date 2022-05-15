using Entities.Model;

namespace Contracts.Services; 

public interface IChatService {
    public Task<ICollection<Chat>> GetAllChats(); // TODO this is just for test
    public Task<Chat> GetOrCreateChat(Guid userOne, Guid UserTwo);
    public Task<Chat> AddGroupChat(Chat chat);
    public Task<Chat> GetChat(Guid CUI);
    public Task<ICollection<Chat>> GetAllChatsByUser(Guid RUIUser);
    public Task<Chat> UpdateChat(Chat chat);
    public Task SetAsReadMessages(Guid myUserRui, Chat chat);
}