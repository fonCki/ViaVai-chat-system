package sep.java.sep_t3.contracts;

import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import sep.java.sep_t3.model.Chat;

import java.util.List;

public interface IChatDao {
    public ResponseEntity<Chat> AddChat(String chatAsJson) throws JsonProcessingException;
    public ResponseEntity<Chat> GetChat(String CID);
    public ResponseEntity<List<Chat>> GetAllChat();
    public ResponseEntity<List<Chat>> GetChatsFromParticularUser(String RUI);
    public ResponseEntity<HttpStatus> DeleteChat(String CID);
    public ResponseEntity<Chat> UpdateChat(String chatAsJson) throws JsonProcessingException;
}