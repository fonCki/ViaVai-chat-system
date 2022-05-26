using Contracts.DAO;
using Contracts.Services;
using Entities.Model;

namespace Application; 

public class MessageServerImp : IMessageService {
    
    private IMessageDao MessageDao;

    private IChatService ChatService; //TODO DELETE THIS
    

    public MessageServerImp(IMessageDao messageDao, IChatService chatService) {
        MessageDao = messageDao;
        ChatService = chatService; //TODO Delete this
    }

    public async Task<Message> SaveMessage(Message message) {
        Chat chat = await ChatService.GetChat(message.Header.CUIRecipient);
        if (chat == null) {
            throw new Exception("Hay un error con el chat");
        }

        await MessageDao.AddMessage(message);
        return message;
    }

    public async Task<ICollection<Message>> GetAllMessage(Guid CUI) { //TODO update this
        ICollection<Message> fullList = await MessageDao.GetAllMessage();
        fullList = fullList.Where(m => m.Header.CUIRecipient.Equals(CUI)).ToList();
        return fullList;
    }
}