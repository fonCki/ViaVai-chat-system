package sep.java.sep_t3.model;

import lombok.Data;
import org.hibernate.annotations.Type;

import javax.persistence.*;

@Data
@Entity
@Table(name = "users")
public class User {
        @Id
        private String rui;

        private String name;

        private String lastName;

        @Lob
        @Type(type = "org.hibernate.type.TextType")
        private String avatar;

        private String email;

        private String password;


        private Status status;

}
