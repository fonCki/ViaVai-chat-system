using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Chat {

    public Guid CID { get; set; } 
    
    public ICollection<User> Subscribers { get; set; }

    public ICollection<Message> Messages { get; set; } 

    [JsonConstructor]
    public Chat() {
        CID = Guid.NewGuid();
        Subscribers = new List<User>();
        Messages = new List<Message>();
    }
    
    public bool IsAGroup() {
        return Subscribers.Count > 2;
    }

    public bool IsPrivate() {
        return !IsAGroup();
    }
    
}