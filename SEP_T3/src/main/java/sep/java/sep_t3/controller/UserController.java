package sep.java.sep_t3.controller;


import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import sep.java.sep_t3.contracts.IUserDao;
import sep.java.sep_t3.model.User;
import sep.java.sep_t3.repository.UserRepository;

import java.util.List;
import java.util.Optional;

@CrossOrigin(origins = "http://localhost:8081")
@RestController
@RequestMapping("Api/Dao/User")



public class UserController implements IUserDao {
    @Autowired
    UserRepository userRepository;

    @Override
    @PostMapping()
    public ResponseEntity<User> AddUser(@RequestBody String userAsJson) throws JsonProcessingException {
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

    @Override
    @GetMapping("/{RUI}")
    public ResponseEntity<User> GetUser(@PathVariable("RUI") String RUI) {
        Optional<User> findedUser = userRepository.findById(RUI);
        if (findedUser.isPresent()) {
            return new ResponseEntity<>(findedUser.get(), HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }

    }

    @Override
    @DeleteMapping("/{RUI}")
    public ResponseEntity<HttpStatus> DeleteUser(@PathVariable("RUI") String RUI) {
        try {
            userRepository.deleteById(RUI);
            return new ResponseEntity<>(HttpStatus.ACCEPTED);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
    }


    @Override
    @PatchMapping
    public ResponseEntity<User> UpdateUser(@RequestBody String userAsJson) throws JsonProcessingException {
        ObjectMapper mapper = new ObjectMapper();
        mapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
        mapper.configure(MapperFeature.ACCEPT_CASE_INSENSITIVE_PROPERTIES, true);
        User user = mapper.readValue(userAsJson, User.class);
        Optional<User> findedUser = userRepository.findById(user.getRui());
        if (findedUser.isPresent()) {
            try {
                findedUser.get().setName(user.getName());
                findedUser.get().setLastName(user.getLastName());
                findedUser.get().setAvatar(user.getAvatar());
                findedUser.get().setEmail(user.getEmail());
                findedUser.get().setPassword(user.getPassword());
                findedUser.get().setStatus(user.getStatus());
                return new ResponseEntity<>(userRepository.save(findedUser.get()), HttpStatus.OK);

            } catch (Exception e) {
                return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
            }
        }
        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
    }

    @Override
    @GetMapping
    public ResponseEntity<List<User>> GetAllUsers() {
        List<User> users = userRepository.findAll();
        if (!users.isEmpty()) {
            return new ResponseEntity<>(users, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
