package sep.java.sep_t3.controller;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import sep.java.sep_t3.contracts.IChatDao;
import sep.java.sep_t3.model.Chat;
import sep.java.sep_t3.repository.ChatRepository;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@CrossOrigin(origins = "http://localhost:8081")
@RestController
@RequestMapping("Api/Dao/Chat")
public class ChatController implements IChatDao {

    @Autowired
    ChatRepository chatRepository;

    @Override
    @PostMapping
    public ResponseEntity<Chat> AddChat(@RequestBody String chatAsJson) throws JsonProcessingException {
        ObjectMapper mapper = new ObjectMapper();
        mapper.enable(MapperFeature.ACCEPT_CASE_INSENSITIVE_PROPERTIES);
        mapper.enable(MapperFeature.ACCEPT_CASE_INSENSITIVE_VALUES);
        mapper.configure(MapperFeature.ACCEPT_CASE_INSENSITIVE_PROPERTIES, true);
        mapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
        Chat chat = mapper.readValue(chatAsJson, Chat.class);
        try {
            Chat c = chatRepository.save(chat);
            return new ResponseEntity<>(c, HttpStatus.CREATED);
        } catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @Override
    @GetMapping("/{CUI}")
    public ResponseEntity<Chat> GetChat(@PathVariable("CUI") String CUI) {
        Optional<Chat> findChat = chatRepository.findById(CUI);
        if (findChat.isPresent()) {
            return new ResponseEntity<>(findChat.get(), HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @Override
    @GetMapping
    public ResponseEntity<List<Chat>> GetAllChat() {
        List<Chat> chats = chatRepository.findAll().stream().collect(Collectors.toList());
        if(!chats.isEmpty()) {
            return new ResponseEntity<>(chats, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
    }

    @Override
    @GetMapping("/User/{CUI}")
    public ResponseEntity<List<Chat>> GetChatsFromParticularUser(String RUI) {
        return GetAllChat(); //TODO implement
    }

    @Override
    @DeleteMapping("/{CUI}")
    public ResponseEntity<HttpStatus> DeleteChat(@PathVariable("CUI") String CUI) {
        try {
            chatRepository.deleteById(CUI);
            return new ResponseEntity<>(HttpStatus.ACCEPTED);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }

    }

    @Override
    @PatchMapping
    public ResponseEntity<Chat> UpdateChat(@RequestBody String chatAsJson) throws JsonProcessingException{
        ObjectMapper mapper = new ObjectMapper();
        mapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
        mapper.configure(MapperFeature.ACCEPT_CASE_INSENSITIVE_PROPERTIES, true);
        Chat chat = mapper.readValue(chatAsJson, Chat.class);
        Optional<Chat> findedChat = chatRepository.findById(chat.getCID());
        if (findedChat.isPresent()) {
            try {
                findedChat.get().setName(chat.getName());
                findedChat.get().setImage(chat.getImage());
                findedChat.get().setSubscribers(chat.getSubscribers());
                findedChat.get().setMessages(chat.getMessages());
                findedChat.get().setCreatedBy(chat.getCreatedBy());
                findedChat.get().setCreated(chat.getCreated());
                findedChat.get().setIsGroup(chat.isIsGroup());
                findedChat.get().setIsPrivate(chat.isIsPrivate());
                return new ResponseEntity<>(chatRepository.save(findedChat.get()), HttpStatus.OK);

            } catch (Exception e) {
                return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
            }
        }
        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
    }
        }
