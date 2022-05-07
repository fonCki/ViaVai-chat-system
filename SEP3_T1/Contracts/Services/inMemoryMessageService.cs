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

    public async Task<ICollection<Message>> GetRelatedMessages(User writer, User reader) { //TODO reader must be a recipient type
        if (_messages == null) {
            LoadOrCreate();
        }

        Console.WriteLine(_messages);
        
        //Return a list of, Created by ME directed to READER || Created by READER directed to ME
        var filteredList = _messages.Where(m => (m.Header.CreatedBy.Email.Equals(writer.Email) && m.Header.Recipient.Email.Equals(reader.Email)) ||
                                                        (m.Header.CreatedBy.Email.Equals(reader.Email) && m.Header.Recipient.Email.Equals(writer.Email)))
                                                        .OrderByDescending(m => m.Header.Created)
                                                        .ToList();

        return filteredList;
    }

    public async Task SendMessage(Message message) {
        Console.WriteLine(message.Body);
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