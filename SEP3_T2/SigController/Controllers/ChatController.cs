using Contracts.Services;
using Entities.Model;
using Microsoft.AspNetCore.Mvc;

namespace SEP3_T2.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class ChatController : Controller, IMessageService {
    
    private IChatService chatService;

    public ChatController(IChatService chatService) {
        this.chatService = chatService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Chat>>> GetChats() {
        try {
            ICollection<Chat> chats = await chatService.GetAllChats();
            return Ok(chats);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("user/{userOne}/{userTwo}")]
    public async Task<ActionResult<Chat>> GetOrCreateChat([FromRoute] Guid userOne, Guid userTwo ) {
        try {
            Chat chat = await chatService.GetOrCreateChat(userOne, userTwo);
            return Ok(chat);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("chat/{CUI}")]
    public async Task<ActionResult<Chat>> GetChat([FromRoute] Guid CUI) {
        try {
            Chat chat = await chatService.GetChat(CUI);
            return Ok(chat);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("user/{RUI}/chat")]
    public async Task<ActionResult<ICollection<Chat>>> GetAllChatByUser([FromRoute] Guid RUI) {
        try {
            ICollection<Chat> chat = await chatService.GetAllChatsByUser(RUI);
            return Ok(chat);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult<Chat>> UpdateChat([FromBody] Chat chat) {
        try {
            Chat updatedChat = await chatService.UpdateChat(chat);
            return Ok(updatedChat);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Chat>> CreateGroupChat([FromBody] Chat chat) {
        try {
            Chat newChat = await chatService.CreateGroupChat(chat);
            return Ok(newChat);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    public async Task<Message> SaveMessage(Message message) {
        throw new NotImplementedException();
        
    }
}