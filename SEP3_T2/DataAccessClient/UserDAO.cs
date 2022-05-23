using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Contracts.DAO;
using Entities.Address;
using Entities.Model;

namespace RESTClient; 

public class UserDAO : IUserDao{
    
    public async Task<User> AddUser(User user) {
        using HttpClient client = new();
        string userToJson = JsonSerializer.Serialize(user);
        StringContent content = new(userToJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(Address.ENDPOINT_USER, content);
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

    public async Task<User> GetUser(Guid RUI) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_USER + $"/rui/{RUI}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return user;
    }

    public async Task<User> GetUser(string email) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_USER + $"/email/{email}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return user;
    }

    public async Task<ICollection<User>> GetAllUsers() {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(Address.ENDPOINT_USER);
        string content = await response.Content.ReadAsStringAsync();


        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        
        

        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions {
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        
        return users;
    }

    public async Task DeleteUser(Guid RUI) {
        using HttpClient client = new();
        HttpResponseMessage response = await client.DeleteAsync(Address.ENDPOINT_USER + $"/{RUI}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}");
        }
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
            Converters = {
                new JsonStringEnumConverter( JsonNamingPolicy.CamelCase)
            },
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        })!;
        return returned;
    }


}