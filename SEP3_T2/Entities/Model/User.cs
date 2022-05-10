using System.Text.Json.Serialization;

namespace Entities.Model; 

public class User : Recipient {
    
    public override string Name { get; set; }
    
    public override string Avatar { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Status Status { get; set; }
    
    [JsonConstructor]
    public User() { }
    
    public User(string fName, string lastName, string email, string password, string avatar) {
        Name = fName;
        LastName = lastName;
        Email = email;
        Password = password;
        Avatar = avatar;
        Status = Status.Offline;
    }
    
    public User(string fName, string lastName, string email, string password) {
        Name = fName;
        LastName = lastName;
        Email = email;
        Password = password;
        Avatar = "images/-user-login.png"; //Default Avatar
        Status = Status.Offline;
    }

   
}