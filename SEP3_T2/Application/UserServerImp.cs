using Contracts.DAO;
using Contracts.Services;
using Entities.Model;

namespace Application; 

public class UserServerImp : IUserService {
    private IUserDao UserDao;

    public UserServerImp(IUserDao userDao) {
        UserDao = userDao;
    }

    public async Task<ICollection<User>> GetContactList() {
        return await UserDao.GetAllUsers();
    }

    public async Task<User> GetUserAsyncByEmail(string email) {
        return await UserDao.GetUser(email);
    }

    public async Task<User> GetUserAsyncByRUI(Guid RUI) {
        return await UserDao.GetUser(RUI);
    }

    public async Task<User> SignUp(string name, string lname, string email, string password, string imgPath) {
        return await UserDao.AddUser(new User(name, lname, email, password, imgPath));
    }

    public async Task<User> UpdateUser(User user) {
        return await UserDao.UpdateUser(user);
    }

    public async Task DeleteAccount(User user) {
        await UserDao.DeleteUser(user.RUI);
    }

    public async Task<Status> SetStatus(Guid RUI, Status status) {
        User user = await UserDao.GetUser(RUI);
        user.Status = status;
        await UpdateUser(user);
        return user.Status;
    }
}