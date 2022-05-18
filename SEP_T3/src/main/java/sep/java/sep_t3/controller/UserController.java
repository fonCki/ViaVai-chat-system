package sep.java.sep_t3.controller;


import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import sep.java.sep_t3.model.Message;
import sep.java.sep_t3.model.User;
import sep.java.sep_t3.repository.ChatRepository;
import sep.java.sep_t3.repository.UserRepository;

@CrossOrigin(origins = "http://localhost:8081")
@RestController
@RequestMapping("/Users")



public class UserController {
    @Autowired
    UserRepository userRepository;

    @PostMapping("/Add")
    public ResponseEntity<User> saveMessage(@RequestBody String userAsJson) throws JsonProcessingException {
        System.out.println(userAsJson);
        ObjectMapper mapper = new ObjectMapper();
        mapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
        mapper.configure(MapperFeature.ACCEPT_CASE_INSENSITIVE_PROPERTIES, true);
        User user = mapper.readValue(userAsJson, User.class);
        try {
            User u = userRepository.save(user);
            return new ResponseEntity<>(u, HttpStatus.CREATED);
        } catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
