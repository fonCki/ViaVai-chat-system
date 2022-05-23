using Entities.Model;

namespace Contracts.DAO; 

public interface IUserDao {
    public Task<User> AddUser(User user);
    public Task<User> GetUser(Guid RUI);
    public Task<User> GetUser(string email);
    public Task<ICollection<User>> GetAllUsers();
    public Task DeleteUser(Guid RUI);
    public Task<User> UpdateUser(User user);
}
