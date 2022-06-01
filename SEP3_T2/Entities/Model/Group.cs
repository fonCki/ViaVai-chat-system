namespace Entities.Model; 

public class Group : Recipient {
    
    public ICollection<User> Members { get; set; }
    public override string Name { get; set; }
    public override string Avatar { get; set; }
    public override Guid RUI { get; set; }
}