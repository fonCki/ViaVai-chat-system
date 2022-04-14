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

    @Column(name = "body")
    private String body;

    @Column(name="created")
    private Date created;

    public Message() {
    }

    public Message(long id, String body) {
        this.id = id;
        this.body = body;
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

    @Override
    public String toString() {
        return "Message{" +
                "id=" + id +
                ", body='" + body + '\'' +
                '}';
    }
}