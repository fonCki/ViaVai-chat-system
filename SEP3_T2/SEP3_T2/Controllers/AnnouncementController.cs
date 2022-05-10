using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SEP3_T2.Controllers; 

public class AnnouncementController : Controller {
    private IHubContext<ChatHub> _hubContext;

    public AnnouncementController(IHubContext<ChatHub> hubContext) {
        _hubContext = hubContext;
    }

    [HttpGet("/announcement")]
    public IActionResult Index() {
        return null;
    }

    [HttpPost("/announcement")]
    public async Task<IActionResult> Post([FromQuery] string message) {
        await _hubContext.Clients.All.SendAsync("Broadcast", message);
        Console.WriteLine(message);
        return Ok();
    }
}