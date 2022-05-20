using Contracts.DAO;
using Contracts.Services;
using Entities.Model;

namespace Application; 

public class ChatServiceImp : IChatService {
    private IChatDao ChatDao;

    public ChatServiceImp(IChatDao chatDao) {
        ChatDao = chatDao;
    }

    public async Task<ICollection<Chat>> GetAllChats() {
        throw new NotImplementedException();
    }

    public async Task SaveMessage(Message message) {
        throw new NotImplementedException();
    }

    public async Task<Chat> GetOrCreateChat(Guid userOne, Guid UserTwo) {
        throw new NotImplementedException();
    }

    public async Task<Chat> GetChat(Guid CUI) {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Chat>> GetAllChatsByUser(Guid RUIUser) {
        throw new NotImplementedException();
    }

    public async Task<Chat> UpdateChat(Chat chat) {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Guid>> GetAllUsersFromChat(Guid CUI) {
        throw new NotImplementedException();
    }

    public async Task<Chat> CreateGroupChat(Chat chat) {
        throw new NotImplementedException();
    }
}