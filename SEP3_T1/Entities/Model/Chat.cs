using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Chat {

    public Guid CID { get; set; } 
    
    public ICollection<User> Members { get; set; }

    public ICollection<Message> Messages { get; set; } 

    [JsonConstructor]
    // public Chat(User Myself, ICollection<User> Members) { TODO Create Group chat
    public Chat() {    
        CID = Guid.NewGuid();
        Members = new List<User>();
        Messages = new List<Message>();
    }

    public bool IsAGroup() {
        return Members.Count > 2;
    }

    public bool IsPrivate() {
        return !IsAGroup();
    }
    
    
    
}