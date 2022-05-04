using System.Text.Json;
using Entities.Model;

namespace JsonDataAccess;

public class JsonContext {
    private ICollection<User> users;
    private ICollection<Message> messages;
    private readonly string userPath = "chat.json";
    private readonly string messagePath = "message.json";

    public JsonContext() {
        if (File.Exists(userPath))
            LoadData();
        else
            CreateFile();
    }

    public ICollection<User> Users {
        get {
            if (users == null) LoadData();

            return users!;
        }
        private set { }
    }

    private void CreateFile() {
        users = new List<User>();
        Task.FromResult(SaveChangesAsync());
    }

    private void LoadData() {
        var forumAsJson = File.ReadAllText(userPath);
        users = JsonSerializer.Deserialize<ICollection<User>>(forumAsJson)!;
    }

    public async Task SaveChangesAsync() {
        var forumAsJson = JsonSerializer.Serialize(users, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(userPath, forumAsJson);
        users = null;
    }
}