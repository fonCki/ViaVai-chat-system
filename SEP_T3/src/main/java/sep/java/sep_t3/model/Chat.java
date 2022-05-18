package sep.java.sep_t3.model;

import javax.persistence.*;
import java.util.Date;
import java.util.List;


@Entity
@Table(name = "chats")

public class Chat {
    @Id
    public String CID;

    public String name;

    public String image;

    @OneToMany
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
    boolean isGroup;
    @Column(name = "IsPrivate")
    boolean isPrivate;

    public Chat() {
    }

    public String getCID() {
        return CID;
    }

    public void setCID(String CID) {
        this.CID = CID;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public List<User> getSubscribers() {
        return subscribers;
    }

    public void setSubscribers(List<User> subscribers) {
        this.subscribers = subscribers;
    }

    public List<Message> getMessages() {
        return messages;
    }

    public void setMessages(List<Message> messages) {
        this.messages = messages;
    }

    public User getCreatedBy() {
        return createdBy;
    }

    public void setCreatedBy(User createdBy) {
        this.createdBy = createdBy;
    }

    public Date getCreated() {
        return created;
    }

    public void setCreated(Date created) {
        this.created = created;
    }

    public boolean isGroup() {
        return isGroup;
    }

    public void setGroup(boolean group) {
        isGroup = group;
    }

    public boolean isPrivate() {
        return isPrivate;
    }

    public void setPrivate(boolean aPrivate) {
        isPrivate = aPrivate;
    }
}
