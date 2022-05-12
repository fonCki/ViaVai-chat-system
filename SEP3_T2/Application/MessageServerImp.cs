using Contracts.Services;
using Entities.Model;

namespace Application; 

public class MessageServerImp : IMessageService {
    
    private IChatService ChatService;


    public MessageServerImp(IChatService chatService) {
        ChatService = chatService;
    }

    public async Task<Message> SaveMessage(Message message) {
        Chat chat = await ChatService.GetChat(message.Header.CUIRecipient);
        if (chat == null) {
            throw new Exception("Hay un error con el chat");
        }
        await ChatService.SaveMessage(message);
        return message;
    }
}