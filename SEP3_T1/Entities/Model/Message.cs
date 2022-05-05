namespace Entities.Model; 

public class Message {
    public DateTime Created { get; }
    public User CreatedBy { get; }
    public string Body { get; }
    
    public bool Read { get; set; }
    
    public Message(DateTime created, User createdBy, string body) {
        Created = created;
        CreatedBy = createdBy;
        Body = body;
        Read = false;
    }

    public override string ToString() {
        return $"{nameof(Created)}: {Created}, {nameof(CreatedBy)}: {CreatedBy}, {nameof(Body)}: {Body}";
    }
}