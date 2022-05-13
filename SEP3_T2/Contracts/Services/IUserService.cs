using Entities.Model;

namespace Contracts.Services; 

public interface IUserService {
    public Task<ICollection<User>> GetContactList();
    public Task<User> GetUserAsyncByEmail(string email);
    public Task<User> GetUserAsyncByRUI(Guid RUI);
    public Task<User> SignUp(string name, string lname, string email, string password, string imgPath);
    public Task<User> UpdateUser(User user);
    public Task DeleteAccount(User user);
    public Task<Status> SetStatus(Guid RUI, Status status);

}
