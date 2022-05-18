package sep.java.sep_t3.model;

import com.fasterxml.jackson.annotation.JsonFormat;

import javax.persistence.*;
import java.text.DateFormat;
import java.util.Date;

@Entity
@Table(name = "messages")
public class Message {
    @Id
    private String MID;

    @OneToOne
    @JoinColumn(name = "Header")
    public Header header;

    private String body;

    private boolean read;


    public String getMID() {
        return MID;
    }


    public void setMID(String MID) {
        this.MID = MID;
    }

    public Header getHeader() {
        return header;
    }

    public void setHeader(Header header) {
        this.header = header;
    }

    public String getBody() {
        return body;
    }

    public void setBody(String body) {
        this.body = body;
    }

    public boolean isRead() {
        return read;
    }

    public void setRead(boolean read) {
        this.read = read;
    }
}