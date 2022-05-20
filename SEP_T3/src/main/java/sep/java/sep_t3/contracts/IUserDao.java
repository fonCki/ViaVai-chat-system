package sep.java.sep_t3.contracts;

import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import sep.java.sep_t3.model.User;

import java.util.List;

public interface IUserDao {
    public ResponseEntity<User> AddUser(String userAsJson) throws JsonProcessingException;
    public ResponseEntity<User> GetUser(String Email);
    public ResponseEntity<HttpStatus> DeleteUser(String email);
    public ResponseEntity<User> UpdateUser(String userAsJson) throws JsonProcessingException;
    public ResponseEntity<List<User>> GetAllUsers();
}

