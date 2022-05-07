using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Header {
    public Recipient Recipient { get; set; }
    public User CreatedBy { get; set; }
    public DateTime Created { get; set; }

    [JsonConstructor]
    public Header() { }

    public Header(Recipient recipient, User createdBy) {
        Recipient = recipient;
        CreatedBy = createdBy;
        Created = DateTime.Now;
    }

    public override string ToString() {
        return $"{nameof(Recipient)}: {Recipient}, {nameof(CreatedBy)}: {CreatedBy}, {nameof(Created)}: {Created}";
    }
}