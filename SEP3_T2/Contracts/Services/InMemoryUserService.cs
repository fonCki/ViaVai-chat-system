using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Entities.Model;


namespace Contracts.Services; 

public class InMemoryUserService : IUserService {

    private string usersPath = "/Users/alfonsoridao/Library/CloudStorage/OneDrive-ViaUC/Via University/Semester 3/SEP3/Sep3_Project/SEP3_T2/Contracts/Services/JsonFiles/users.json";

    private ICollection<User>? _users;

    public InMemoryUserService() {
         LoadOrCreate();
    }

    

    public async Task<ICollection<User>> GetContactList() {
        if (_users == null) {
            await LoadOrCreate();
        }
        return _users;
    }

    // public async Task<User> GetUserAsyncByEmail(string email) {
    //     if (String.IsNullOrEmpty(email)) {
    //         throw new Exception("Error with user address");
    //     }
    //     User? find = _users.FirstOrDefault(user => user.Email.Equals(email));
    //     if (find == null) {
    //         throw new Exception("User not found");
    //     }
    //     return find;
    // }

    public async Task<User> GetUserAsyncByRUI(Guid RUI) {
        if (_users == null) {
            await LoadOrCreate();
        }
        if (RUI == null) {
            throw new Exception("Error with user address");
        }
        User? find = _users.FirstOrDefault(user => user.RUI.Equals(RUI));
        if (find == null) {
            throw new Exception("User not found");
        }
        return find;
    }
    


    // public async Task<User> SignUp(string name, string lname, string email, string password, string imgPath) {
    //     if (await existUser(email)) {
    //         throw new Exception("User already exist");
    //     }
    //
    //     if (_users == null) {
    //         await LoadOrCreate();
    //     }
    //     _users.Add(new User(name, lname, email, password, imgPath));
    //     await SaveChangesAsync();
    //     return _users.FirstOrDefault(user => user.Email.Equals(email));
    // }
    
    public async Task<User> GetUserAsyncByEmail(string email) {
        using HttpClient client = new();
        
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/Users/{email}");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
        
        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return returned;
    }
    
    
    public async Task<User> SignUp(string name, string lname, string email, string password, string imgPath) {
        User newUser = new User(name, lname, email, password, imgPath);
        using HttpClient client = new();
        string userToJson = JsonSerializer.Serialize(newUser);
        StringContent content = new(userToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"http://localhost:8080/Users", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;


        return returned;
    }

    public async Task<User> UpdateUser(User user) {
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
        await SaveChangesAsync();
        return find;
    }

    public async Task DeleteAccount(User user) {
        if (_users == null) {
            await LoadOrCreate();
        }
        User? find = _users.FirstOrDefault(user => user.Email.Equals(user.Email));
        _users.Remove(find);
        await SaveChangesAsync();
    }

    public async Task<Status> SetStatus(Guid RUI, Status status) {
        if (_users == null) {
            await LoadOrCreate();
        }
        if (RUI == null) {
            throw new Exception("Error with user address");
        }
        User? find = _users.FirstOrDefault(user => user.RUI.Equals(RUI));
        if (find == null) {
            throw new Exception("User not found");
        }
        find.Status = status;
        await SaveChangesAsync();
        return find.Status;
    }


    private async Task<bool> existUser(string email) {
        if (_users == null) {
            await LoadOrCreate();
        }
        return _users!.Any(u => email.Equals(u.Email));
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
            await Task.FromResult(SaveChangesAsync());
        }
        
    }
}