package sep.java.sep_t3.contracts;

import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import sep.java.sep_t3.model.Message;

import java.util.List;

public interface IMessageDao {
    public ResponseEntity<Message> AddMessage(String messageAsJson) throws JsonProcessingException;
    public ResponseEntity<Message> GetMessage(String MID);
    public ResponseEntity<List<Message>> GetAllMessage();
    public ResponseEntity<HttpStatus> DeleteMessage(String MID);
    public ResponseEntity<Message> UpdateMessage(String messageAsJson) throws JsonProcessingException;
}
