using Entities.Model;

namespace Contracts.Services; 

public interface IChatService {
    public Task<ICollection<Chat>> GetAllChats();
    public Task SendMessage(Message message);
    
    public Task<Chat> GetOrCreateChat(User userOne, User userTwo); 
}