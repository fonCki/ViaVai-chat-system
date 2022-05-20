package sep.java.sep_t3.controller;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.*;
import sep.java.sep_t3.contracts.IMessageDao;
import sep.java.sep_t3.model.Message;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import sep.java.sep_t3.model.User;
import sep.java.sep_t3.repository.MessageRepository;

import java.util.List;
import java.util.Optional;


@CrossOrigin(origins = "http://localhost:8081")
@RestController
@RequestMapping("Api/Dao/Message")
public class MessageController implements IMessageDao {

    @Autowired
    MessageRepository messageRepository;


    @Override
    @PostMapping
    public ResponseEntity<Message> AddMessage(@RequestBody String messageAsJson) throws JsonProcessingException {
        ObjectMapper mapper = new ObjectMapper();
        mapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
        mapper.configure(MapperFeature.ACCEPT_CASE_INSENSITIVE_PROPERTIES, true);
        Message message = mapper.readValue(messageAsJson, Message.class);
        try {
            Message m = messageRepository.save(message);
            return new ResponseEntity<>(m, HttpStatus.CREATED);
        } catch (Exception e) {
            System.out.println(e.getMessage());
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }

    @Override
    @GetMapping("/{MUI}")
    public ResponseEntity<Message> GetMessage(@PathVariable("MUI") String MID) {
        Optional<Message> findMessage = messageRepository.findById(MID);
        if (findMessage.isPresent()) {
            System.out.println(findMessage.get().header);
            return new ResponseEntity<>(findMessage.get(), HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @Override
    @GetMapping
    public ResponseEntity<List<Message>> GetAllMessage() {
        List<Message> messages = messageRepository.findAll();
        if(!messages.isEmpty()) {
            return new ResponseEntity<>(messages, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
    }

    @Override
    @DeleteMapping("/{MUI}")
    public ResponseEntity<HttpStatus> DeleteMessage(@PathVariable("MUI") String MUI) {
        try {
            messageRepository.deleteById(MUI);
            return new ResponseEntity<>(HttpStatus.ACCEPTED);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
    }

    @Override
    @PatchMapping
    public ResponseEntity<Message> UpdateMessage(@RequestBody String messajeAsJson) throws JsonProcessingException{
        ObjectMapper mapper = new ObjectMapper();
        mapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
        mapper.configure(MapperFeature.ACCEPT_CASE_INSENSITIVE_PROPERTIES, true);
        Message message = mapper.readValue(messajeAsJson, Message.class);
        Optional<Message> findedMessage = messageRepository.findById(message.getMID());
        if (findedMessage.isPresent()) {
            try {
                findedMessage.get().setHeader(message.getHeader());
                findedMessage.get().setBody(message.getBody());
                findedMessage.get().setRead(message.isRead());
                return new ResponseEntity<>(messageRepository.save(findedMessage.get()), HttpStatus.OK);

            } catch (Exception e) {
                return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
            }
        }
        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
    }
}
