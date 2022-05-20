package sep.java.sep_t3.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import sep.java.sep_t3.model.Message;
import sep.java.sep_t3.model.User;

import java.util.List;

@Repository
public interface UserRepository extends JpaRepository<User, String> {
    public User findUsersByEmail(String email);

}