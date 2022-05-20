package sep.java.sep_t3.model;

import lombok.Data;

import javax.persistence.*;
import java.util.Date;

@Data
@Entity
@Table(name = "headers")
public class Header {

    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    public Long HUID;

    public String CUIRecipient;

    @OneToOne
    @JoinColumn(name = "created_by")
    public User createdBy;

    public Date created;
}
