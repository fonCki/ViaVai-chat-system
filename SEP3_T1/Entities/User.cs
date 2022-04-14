namespace Entities; 

public class User {
    public string NickName { get; set; }
    public string Email { get; set; }

    public User(string nickName, string email) {
        NickName = nickName;
        Email = email;
    }
}