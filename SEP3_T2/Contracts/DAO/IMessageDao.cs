using Entities.Model;

namespace Contracts.DAO; 

public interface IMessageDao {
    public Task<Message> AddMessage(Message message);
    public Task<Message> GetMessage(Guid MID);
    public Task<ICollection<Message>> GetAllMessage();
    public Task DeleteMessage(Guid MID);
    public Task<Message> UpdateMessage(Message message);
}