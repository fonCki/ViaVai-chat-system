package sep.java.sep_t3.model;

import javax.persistence.*;
import java.util.Date;

@Entity
@Table(name = "headers")
public class Header {
    @Id
    public String CUIReceipient;
    @OneToOne
    @JoinColumn(name = "created_by")
    public User createdBy;
    public Date created;

    public Header() {
    }

    public String getCUIReceipient() {
        return CUIReceipient;
    }

    public void setCUIReceipient(String CUIReceipient) {
        this.CUIReceipient = CUIReceipient;
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
}
