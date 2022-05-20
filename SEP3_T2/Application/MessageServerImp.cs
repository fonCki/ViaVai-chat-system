using Contracts.DAO;
using Contracts.Services;
using Entities.Model;

namespace Application; 

public class MessageServerImp : IMessageService {
    
    private IMessageDao messageDao;

    private IChatService ChatService; //TODO DELETE THIS

    public MessageServerImp(IMessageDao messageDao, IChatService chatService) {
        this.messageDao = messageDao;
        this.ChatService = chatService; //TODO Delete this
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