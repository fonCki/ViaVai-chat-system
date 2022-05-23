using System.ComponentModel.Design;
using Entities.Model;

namespace Contracts.DAO; 

public interface IChatDao {
    public Task<Chat> AddChat(Chat chat);
    public Task<Chat> GetChat(Guid CID);
    public Task<ICollection<Chat>> GetAllChat();
    public Task<ICollection<Chat>> GetChatsFromParticularUser(Guid RUI);
    public Task DeleteChat(Guid CID);
    public Task<Chat> UpdateChat(Chat chat);
}
