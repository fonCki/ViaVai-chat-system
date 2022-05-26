using Contracts.Services;
using Entities.Model;
using Microsoft.AspNetCore.Mvc;

namespace SEP3_T2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : Controller {

    private IMessageService MessageService;

    public MessageController(IMessageService messageService) {
        MessageService = messageService;
    }

    [HttpGet]
    [Route("Chat/{CUI}")]
    public async Task<ActionResult<ICollection<Message>>> GetMessages([FromRoute] Guid CUI) {
        try {
            ICollection<Message> messages = await MessageService.GetAllMessage(CUI);
            return Ok(messages);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}