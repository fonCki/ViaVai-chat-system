package sep.java.sep_t3.model;

import javax.persistence.*;


@Entity
@Table(name = "users")
public class User {
        @Id
        private long RID;

        private String name;

        private String lastName;

        @Lob
        @Column
        private String avatar;

        private String email;

        private String password;


        private Status status;


    public User() {
    }

    public long getRID() {
        return RID;
    }

    public void setRID(long RID) {
        this.RID = RID;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getAvatar() {
        return avatar;
    }

    public void setAvatar(String avatar) {
        this.avatar = avatar;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

//    public Status getStatus() {
//        return status;
//    }
//
//    public void setStatus(Status status) {
//        this.status = status;
//    }
}
