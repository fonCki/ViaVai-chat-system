using Entities.Model;

namespace Contracts.Services; 

public interface IUserService {
    public Task<User> GetByUserAsyncByEmail(string email);
}