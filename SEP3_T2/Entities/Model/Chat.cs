using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Chat {

    public Guid CID { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public ICollection<User> Subscribers { get; set; } 
    public ICollection<Message> Messages { get; set; }

    public Chat(string name, string image) {
        Name = name;
        Image = image;
        CID = Guid.NewGuid();
        Subscribers = new List<User>();
        Messages = new List<Message>();
    }
    
    [JsonConstructor]
    public Chat() {
        Name = String.Empty;
        Image = "images/-user-login.png";
        CID = Guid.NewGuid();
        Subscribers = new List<User>();
        Messages = new List<Message>();
    }

}