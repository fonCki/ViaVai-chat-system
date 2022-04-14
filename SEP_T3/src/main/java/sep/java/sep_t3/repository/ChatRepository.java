package sep.java.sep_t3.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import sep.java.sep_t3.model.Message;

import java.util.List;

@Repository
public interface ChatRepository extends JpaRepository<Message, Long> {
    List<Message> findMessagesByBodyContains(String search);
}
