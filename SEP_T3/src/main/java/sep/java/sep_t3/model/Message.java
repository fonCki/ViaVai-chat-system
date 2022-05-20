package sep.java.sep_t3.model;
import lombok.Data;
import javax.persistence.*;

@Data
@Entity
@Table(name = "messages")
public class Message {
    @Id
    private String MID;

    @OneToOne(cascade = {CascadeType.ALL})
    @JoinColumn(name = "HUID")
    public Header header;

    private String body;

    private boolean read;

}