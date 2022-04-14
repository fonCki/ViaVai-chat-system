namespace Entities; 

public class Message { 
    public DateTime Created { get; }
    public User CreatedBy { get; }
    public string Body { get; }
    public Message(DateTime created, User createdBy, string body) {
        Created = created;
        CreatedBy = createdBy;
        Body = body;
    }
}