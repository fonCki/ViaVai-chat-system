package sep.java.sep_t3.model;

import javax.persistence.*;


@Entity
@Table(name = "Users")
public class User {
        @Id
        @GeneratedValue(strategy = GenerationType.AUTO)
        private long UID;

        private String email;

        private String nickName;

    public User() {
    }

    public User(String email, String nickName) {
        this.email = email;
        this.nickName = nickName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getNickName() {
        return nickName;
    }

    public void setNickName(String nickName) {
        this.nickName = nickName;
    }
}
