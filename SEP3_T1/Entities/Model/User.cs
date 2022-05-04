namespace Entities.Model; 

public class User {
    public User() { }
    
    public User(string fName, string lastName, string email, string password, string imgPath) {
        FirstName = fName;
        LastName = lastName;
        Email = email;
        ImagePath = imgPath;
    }
    
    public User(string fName, string lastName, string email, string password) {
        FirstName = fName;
        LastName = lastName;
        Email = email;
        ImagePath = "images/-user-login.png";
    }

    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public string ImagePath { get; set; }
    
}