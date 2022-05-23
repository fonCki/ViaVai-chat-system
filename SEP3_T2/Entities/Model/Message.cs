using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Message {
    
    public Guid MID { get; set; }
    
    public Header Header { get; set; }
    
    public string Body { get; set; }
    
    public bool Read { get; set; }


    [JsonConstructor]
    public Message() { }

    public Message(User createdBy, Guid CUIRecipient, string body) {
        MID = Guid.NewGuid(); 
        Header = new Header(CUIRecipient, createdBy);
        Body = body;
        Read = false;
    }

    public override string ToString() {
        return $"{nameof(MID)}: {MID}, {nameof(Header)}: {Header}, {nameof(Body)}: {Body}, {nameof(Read)}: {Read}";
    }
}