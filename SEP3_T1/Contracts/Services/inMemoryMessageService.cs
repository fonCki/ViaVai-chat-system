using System.Text.Json;
using Entities.Model;

namespace Contracts.Services; 

public class inMemoryMessageService : IMessageService {
    
    private string msgPath = "/Users/alfonsoridao/Library/CloudStorage/OneDrive-ViaUC/Via University/Semester 3/SEP3/Sep3_Project/SEP3_T1/Contracts/Services/messages.json";

    private ICollection<Message> _messages;

    public inMemoryMessageService() {
        LoadOrCreate();
    }

    public async Task<ICollection<Message>> GetAllMessages() {
        if (_messages == null) {
            LoadOrCreate();
        }

        return _messages;
    }

    public async Task SendMessage(Message message) {
        if (_messages == null) {
            LoadOrCreate();
        }

        _messages.Add(message);
        SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync() {
        var msgAsJson = JsonSerializer.Serialize(_messages, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(msgPath, msgAsJson);
        _messages = null;
    }
    
    private async Task LoadOrCreate() {
        if (File.Exists(msgPath)) {
            var msgsAsJson = File.ReadAllText(msgPath);
            _messages = JsonSerializer.Deserialize<ICollection<Message>>(msgsAsJson)!;
        }
        else {
            _messages = new List<Message>();
            Task.FromResult(SaveChangesAsync());
        }
        
    }
}