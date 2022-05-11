using System.Text.Json.Serialization;

namespace Entities.Model; 

public abstract class Recipient {
    public abstract string Name { get; set; }
    public abstract string Avatar { get; set; }
    public abstract Guid RUI { get; set; }
    
}