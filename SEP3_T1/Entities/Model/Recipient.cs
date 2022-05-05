using System.Text.Json.Serialization;

namespace Entities.Model; 

public abstract class Recipient {
    public abstract string Name { get; set; }
    public abstract string Avatar { get; set; }
    
    public Guid RUI { get; set; } = Guid.NewGuid();
    
}