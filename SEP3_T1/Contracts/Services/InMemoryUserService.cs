using System.Text.Json;
using Entities.Model;


namespace Contracts.Services; 

public class InMemoryUserService : IUserService {

    private string usersPath = "/Users/alfonsoridao/Library/CloudStorage/OneDrive-ViaUC/Via University/Semester 3/SEP3/Sep3_Project/SEP3_T1/Contracts/Services/JsonFiles/users.json";

    private ICollection<User> _users;

    public InMemoryUserService() {
         LoadOrCreate();
    }

    

    public async Task<ICollection<User>> GetContactList() {
        if (_users == null) {
            await LoadOrCreate();
        }

        return _users;
    }

    public async Task<User> GetUserAsyncByEmail(string email) {
        if (String.IsNullOrEmpty(email)) {
            throw new Exception("Error with user address");
        }
        User? find = _users.FirstOrDefault(user => user.Email.Equals(email));
        if (find == null) {
            throw new Exception("User not found");
        }
        return find;
    }

    public async Task SignUp(string name, string lname, string email, string password, string imgPath) {
        if (await existUser(email)) {
            throw new Exception("User already exist");
        }

        if (_users == null) {
            await LoadOrCreate();
        }

        _users.Add(new User(name, lname, email, password, imgPath));
            SaveChangesAsync();
        
    }

    public async Task UpdateUser(User user) {
        Console.WriteLine(user);
        if (_users == null) {
            await LoadOrCreate();
        }
        User? find = _users.FirstOrDefault(user => user.Email.Equals(user.Email));
        if (find == null) {
            throw new Exception("System Update error");
        }

        find.Name = user.Name;
        find.LastName = user.LastName;
        find.Email = user.Email;
        find.Password = user.Password;
        SaveChangesAsync();
    }

    public async Task DeleteAccount(User user) {
        if (_users == null) {
            await LoadOrCreate();
        }
        User? find = _users.FirstOrDefault(user => user.Email.Equals(user.Email));
        _users.Remove(find);
        SaveChangesAsync();
    }




    private async Task<bool> existUser(string email) {
        if (_users == null) {
            await LoadOrCreate();
        }
        return _users.Any(u => email.Equals(u.Email));
    }
    
    public async Task SaveChangesAsync() {
        var usersAsJson = JsonSerializer.Serialize(_users, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(usersPath, usersAsJson);
        _users = null;
    }

    private async Task LoadOrCreate() {
        if (File.Exists(usersPath)) {
            var usersAsJson = File.ReadAllText(usersPath);
            _users = JsonSerializer.Deserialize<ICollection<User>>(usersAsJson)!;
        }
        else {
            _users = new List<User>();
            Task.FromResult(SaveChangesAsync());
        }
        
    }
}