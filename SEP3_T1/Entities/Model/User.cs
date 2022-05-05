using System.Text.Json.Serialization;

namespace Entities.Model; 

public class User {
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public string ImagePath { get; set; }

    public Status Status { get; set; } = Status.Offline;
    
    [JsonConstructor]
    public User() { }
    
    public User(string fName, string lastName, string email, string password, string imgPath) {
        FirstName = fName;
        LastName = lastName;
        Email = email;
        Password = password;
        ImagePath = imgPath;
    }
    
    public User(string fName, string lastName, string email, string password) {
        FirstName = fName;
        LastName = lastName;
        Email = email;
        Password = password;
        ImagePath = "images/-user-login.png";
    }

    public override string ToString() {
        return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Email)}: {Email}, {nameof(Password)}: {Password}, {nameof(ImagePath)}: {ImagePath}";
    }
}