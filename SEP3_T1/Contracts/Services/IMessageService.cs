using Entities.Model;

namespace Contracts.Services; 

public interface IMessageService {
    public Task SendMessage(Message message);
    public Task<ICollection<Message>> GetAllMessagesFromChat(Guid CUI);
}