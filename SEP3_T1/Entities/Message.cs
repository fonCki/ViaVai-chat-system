namespace Entities; 

public class Message {
    public string NickName { get; set; }
    public string Body { get; set; }

    public Message(string nickName, string body) {
        NickName = nickName;
        Body = body;
    }
}