using Contracts.Services;
using Entities.Model;

namespace Client; 

public class MessageClient : IMessageService {
    public Task SendMessage(Message message) {
        throw new NotImplementedException();
    }
}