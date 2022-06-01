
using Contracts.DAO;
using Contracts.Services;
using Entities.Model;

namespace Application; 

public class ChatServiceImp : IChatService {
    private IChatDao ChatDao;
    private IUserDao UserDao;

    public ChatServiceImp(IChatDao chatDao, IUserDao userDao) {
        ChatDao = chatDao;
        UserDao = userDao;
    }

    public async Task<ICollection<Chat>> GetAllChats() {
        return await ChatDao.GetAllChat();
    }

    public async Task<Chat> GetOrCreateChat(Guid userOne, Guid userTwo) {
        if (userOne == null || userTwo == null) {
            throw new Exception("User can't be null mate! fix this");
        }

        if (userOne.Equals(userTwo)) {
            throw new Exception("Can't create a chat with the same person");
        }
        
        ICollection<Chat> fullList = await ChatDao.GetAllChat();
        //Filter by single chats
        var singleChats = fullList.Where(c => c.IsPrivate);
        
        //Return a available chat between this 2 users
        var chat = singleChats.Where(c => c.Subscribers.Any(u => u.RUI.Equals(userOne))).Where(c => c.Subscribers.Any(u => u.RUI.Equals(userTwo))).FirstOrDefault();
        
        if (chat == null) {
            User Myself = await UserDao.GetUser(userOne);
            User ChatUser = await UserDao.GetUser(userTwo);
            chat = Chat.CreatePrivate(Myself, ChatUser);
            await ChatDao.AddChat(chat);
        }
        return chat;
    
    }

    public async Task<Chat> GetChat(Guid CUI) {
        return await ChatDao.GetChat(CUI);
    }

    public async Task<ICollection<Chat>> GetAllChatsByUser(Guid RUIUser) {
        return await ChatDao.GetChatsFromParticularUser(RUIUser);
    }

    public async Task<Chat> UpdateChat(Chat chat) {
        return await ChatDao.UpdateChat(chat);
    }

    public async Task<ICollection<Guid>> GetAllUsersFromChat(Guid CUI) {
        Chat chat = await ChatDao.GetChat(CUI);
        ICollection<Guid> returned = new List<Guid>();
        foreach (var user in chat.Subscribers) {
            returned.Add(user.RUI);
        }
        return returned;
    }

    public async Task<Chat> CreateGroupChat(Chat chat) {
        return await ChatDao.AddChat(chat);
    }
}