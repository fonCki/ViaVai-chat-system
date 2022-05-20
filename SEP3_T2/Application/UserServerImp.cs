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
        throw new NotImplementedException();
    }

    public async Task<User> GetUserAsyncByRUI(Guid RUI) {
        throw new NotImplementedException();
    }

    public async Task<User> SignUp(string name, string lname, string email, string password, string imgPath) {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateUser(User user) {
        throw new NotImplementedException();
    }

    public async Task DeleteAccount(User user) {
        throw new NotImplementedException();
    }

    public async Task<Status> SetStatus(Guid RUI, Status status) {
        throw new NotImplementedException();
    }
}