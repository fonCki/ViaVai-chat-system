
using Contracts.DAO;
using Contracts.Services;
using Entities.Model;

namespace Application; 

public class MessageServerImp : IMessageService {
    
    private IMessageDao MessageDao;

    private IChatService ChatService; 
    

    public MessageServerImp(IMessageDao messageDao, IChatService chatService) {
        MessageDao = messageDao;
        ChatService = chatService;
    }

    public async Task<Message> SaveMessage(Message message) {
        Chat chat = await ChatService.GetChat(message.Header.CUIRecipient);
        if (chat == null) {
            throw new Exception("There is and error with the chat");
        }
        await MessageDao.AddMessage(message);
        return message;
    }

    public async Task<ICollection<Message>> GetAllMessage(Guid CUI) { 
        ICollection<Message>? fullList = await MessageDao.GetAllMessage();
        fullList = fullList.Where(m => m.Header.CUIRecipient.Equals(CUI)).ToList();
        return fullList;
    }
}