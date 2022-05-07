using System.Text.Json.Serialization;

namespace Entities.Model; 

public abstract class Recipient {
    // public abstract Guid UID { get; set; }
    public abstract string Name { get; set; }
    public abstract string Avatar { get; set; }
    
}