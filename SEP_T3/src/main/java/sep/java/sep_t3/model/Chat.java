package sep.java.sep_t3.model;

import lombok.Data;

import javax.persistence.*;
import java.util.Date;
import java.util.List;

@Data
@Entity
@Table(name = "chats")

public class Chat {
    @Id
    public String CID;

    public String name;

    public String image;

    @ManyToMany
    @JoinColumn(name = "subscribers")
    public List<User> subscribers;

    @OneToMany
    @JoinColumn(name = "messages")
    public List<Message> messages;

    @OneToOne
    @JoinColumn(name = "created_by")
    public User createdBy;

    public Date created;

    @Column(name = "IsGroup")
    boolean IsGroup;

    @Column(name = "IsPrivate")
    boolean IsPrivate;

}
