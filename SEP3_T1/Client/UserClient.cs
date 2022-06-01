using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Contracts.Services;
using Entities.Address;
using Entities.Model;

namespace Client; 

public class UserClient : IUserService {
    public async Task<ICollection<User>> GetContactList() {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_USER);
        string content = await response.Content.ReadAsStringAsync();


        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public async Task<User> GetUserAsyncByEmail(string email) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_USER + $"/email/{email}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<User> GetUserAsyncByRUI(Guid RUI) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_USER + $"/rui/{RUI}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<User> SignUp(string name, string lname, string email, string password, string imgPath) {
        using HttpClient client = new();
        User newUser = new User(name, lname, email, password, imgPath);
        Console.WriteLine("From SignUp: " + newUser.Password);
        string userToJson = JsonSerializer.Serialize(newUser);
        StringContent content = new(userToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(Address.ENDPOINT_USER, content);
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
        using HttpClient client = new();
        string userToJson = JsonSerializer.Serialize(user);
        StringContent content = new(userToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PatchAsync(Address.ENDPOINT_USER, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        User returned = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return returned;
    }

    public async Task DeleteAccount(User user) {
        throw new NotImplementedException();
    }

    public async Task<Status> SetStatus(Guid RUI, Status status) {
        Console.WriteLine(status);
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_USER + $"/set/{RUI}/{status}");
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        Status returned = JsonSerializer.Deserialize<Status>(responseContent, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine("Returned: " + returned);
        return returned;
    }
}