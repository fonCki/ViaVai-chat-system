using Entities.Model;

namespace Contracts.Services; 

public interface IUserService {
    public Task<User> GetUserAsyncByEmail(string email);
    public Task SignUp(string name, string lname, string email, string password, string imgPath);
}
