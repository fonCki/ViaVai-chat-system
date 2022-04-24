package sep.java.sep_t3.model;

import com.fasterxml.jackson.annotation.JsonFormat;

import javax.persistence.*;
import java.text.DateFormat;
import java.util.Date;

@Entity
@Table(name = "Message")
public class Message {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;

    @Column(name = "Body")
    private String body;

    @Column(name="Created")
    private Date created;

//    @ManyToOne(cascade = CascadeType.ALL)
//    @JoinColumn(name = "UID", referencedColumnName = "UID")
//    private User createdBy;

    public Message() {
    }

    public long getId() {
        return id;
    }

    public String getBody() {
        return body;
    }

    public void setBody(String body) {
        this.body = body;
    }

    public Date getCreated() {
        return created;
    }

    public void setCreated(Date created) {
        this.created = created;
    }

    @Override
    public String toString() {
        return "Message{" +
                "id=" + id +
                ", body='" + body + '\'' +
                ", created=" + created +
              //   ", createdBy=" + createdBy +
                '}';
    }
}