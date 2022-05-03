using Entities.Model;

namespace Contracts.Services; 

public class InMemoryUserService : IUserService {




    public async Task<User> GetByUserAsyncByEmail(string email) {
        User? find = users.Find(user => user.Email.Equals(email));
        return find;
    }
    
    private List<User> users = new()
    {
        new User("Chuky", "Jose", "Lacambra", "Jhon@gmail.com","Jhon123", ""),

    };
}