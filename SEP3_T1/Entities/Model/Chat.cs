using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Chat {

    public Guid CID { get; set; } 
    public ICollection<User> Subscribers { get; set; } = new List<User>();//TODO should be kind Receipe

    public ICollection<Message> Messages { get; set; } = new List<Message>();

    [JsonConstructor]
    public Chat() {
        CID = Guid.NewGuid();
    }
    
}