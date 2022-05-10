using Entities.Model;

namespace Contracts.Services; 

public interface IUserService {
    public Task<ICollection<User>> GetContactList();
    public Task<User> GetUserAsyncByEmail(string email);
    public Task SignUp(string name, string lname, string email, string password, string imgPath);
    public Task UpdateUser(User user);
    public Task DeleteAccount(User user);

}
