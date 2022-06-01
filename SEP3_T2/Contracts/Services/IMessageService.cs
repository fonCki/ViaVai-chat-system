using Entities.Model;

namespace Contracts.Services; 

public interface IMessageService {
    public Task<Message> SaveMessage(Message message);
    public Task<ICollection<Message>> GetAllMessage(Guid CUI);
}