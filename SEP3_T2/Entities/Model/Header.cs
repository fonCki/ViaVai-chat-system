using System.Text.Json.Serialization;

namespace Entities.Model; 

public class Header {
    public Guid CUIRecipient { get; set; } 
    public User CreatedBy { get; set; }
    public DateTime Created { get; set; }

    [JsonConstructor]
    public Header() { }

    public Header(Guid cuiRecipient, User createdBy) { 
        CUIRecipient = cuiRecipient;
        CreatedBy = createdBy;
        Created = DateTime.Now;
    }

    public override string ToString() {
        return $"{nameof(CUIRecipient)}: {CUIRecipient}, {nameof(CreatedBy)}: {CreatedBy}, {nameof(Created)}: {Created}";
    }
}