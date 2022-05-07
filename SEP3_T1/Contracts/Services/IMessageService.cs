using Entities.Model;

namespace Contracts.Services; 

public interface IMessageService {
    public Task<ICollection<Message>> GetAllMessages();
    public Task SendMessage(Message message);
}