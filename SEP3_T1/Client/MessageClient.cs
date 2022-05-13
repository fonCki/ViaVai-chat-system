using Contracts.Services;
using Contracts.Services.Hub;
using Entities.Model;

namespace Client; 

public class MessageClient : IMessageService {
    public Task SendMessage(Message message) {
        throw new NotImplementedException();
    }
}