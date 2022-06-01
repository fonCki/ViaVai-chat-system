using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Chat {
    public Guid CID { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public ICollection<User> Subscribers { get; set; } 
    public ICollection<Message> Messages { get; set; }
    public User CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public bool IsGroup { get; set; }
    public bool IsPrivate { get; set; }

    private Chat(string name, string image, bool isGroup, User createdBy) {
        CreatedBy = createdBy;
        Created = DateTime.Now;
        CID = Guid.NewGuid();
        Name = name;
        Image = image;
        IsGroup = isGroup;
        IsPrivate = !isGroup;
        Subscribers = new List<User>();
        Subscribers.Add(createdBy);
        Messages = new List<Message>();
    }

    [JsonConstructor]
    public Chat() { }

    public static Chat CreateGroup(string name, string image, User createdBy) {
        return new Chat(name, image, true, createdBy);
    }
    
    public static Chat CreatePrivate(User userOne, User userTwo) {
        Chat newPrivateChat = new Chat($"Private | {userOne.Name} | {userTwo.Name}",
            "images/-user-login.png",
            false, 
            userOne);
        newPrivateChat.Subscribers.Add(userTwo);
        return newPrivateChat;
    }


}