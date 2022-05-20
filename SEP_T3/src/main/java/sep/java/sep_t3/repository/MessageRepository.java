package sep.java.sep_t3.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import sep.java.sep_t3.model.Chat;
import sep.java.sep_t3.model.Message;

@Repository
public interface MessageRepository extends JpaRepository<Message, String> {

}
