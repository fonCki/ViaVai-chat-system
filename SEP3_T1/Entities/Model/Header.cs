using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Header {
    public User Recipient { get; set; } //TODO receipient must be parent class -- JSON ERROR
    public User CreatedBy { get; set; }
    public DateTime Created { get; set; }

    [JsonConstructor]
    public Header() { }

    public Header(User recipient, User createdBy) { //TODO receipient must be parent class -- JSON ERROR
        Recipient = recipient;
        CreatedBy = createdBy;
        Created = DateTime.Now;
    }

    public override string ToString() {
        return $"{nameof(Recipient)}: {Recipient}, {nameof(CreatedBy)}: {CreatedBy}, {nameof(Created)}: {Created}";
    }
}