using System.Security.Claims;
using Contracts.Services;
using Entities;
using Entities.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SEP3_T2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase, IUserService {

    private IUserService UserService;

    public UserController(IUserService userService) {
        UserService = userService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<ICollection<User>>> GetContactList() {
        try {
            ICollection<User> users = await UserService.GetContactList();
            return Ok(users);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    public Task<User> GetUserAsyncByEmail(string email) {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsyncByRUI(Guid RUI) {
        throw new NotImplementedException();
    }

    public Task<User> SignUp(string name, string lname, string email, string password, string imgPath) {
        throw new NotImplementedException();
    }

    Task<User> IUserService.UpdateUser(User user) {
        throw new NotImplementedException();
    }

    public Task DeleteAccount(User user) {
        throw new NotImplementedException();
    }

    Task<Status> IUserService.SetStatus(Guid RUI, Status status) {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("email/{email}")]
    public async Task<ActionResult<User>> GetUserByEmail([FromRoute] string email) {

        try {
            User user = await UserService.GetUserAsyncByEmail(email);
            Console.WriteLine(user + "from controller");
            return Ok(user);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
   
    }
    
    [HttpGet]
    [Route("rui/{rui}")]
    public async Task<ActionResult<User>> GetUserByRuiAsync([FromRoute] Guid rui) {

        try {
            User user = await UserService.GetUserAsyncByRUI(rui);
            return Ok(user);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
   
    }

    [HttpPost]
    public async Task<ActionResult> SignUpAsync([FromBody] User user) {
        try {
             User created = await UserService.SignUp(user.Name,user.LastName, user.Email, user.Password, user.Avatar);
             return Created("User Created", created);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    Task<ICollection<User>> IUserService.GetContactList() {
        throw new NotImplementedException();
    }

    [HttpPatch]
    public async Task<ActionResult<User>> UpdateUser([FromBody] User user) {
        try {
            User updated = await UserService.UpdateUser(user);
            return Ok(user);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("set/{rui}/{status}")]
    public async Task<ActionResult> SetStatus([FromRoute] Guid RUI, Status status) {
        try {
            Status newStatus = await UserService.SetStatus(RUI, status);
            return Ok(newStatus);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}