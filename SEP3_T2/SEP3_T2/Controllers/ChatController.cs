using Microsoft.AspNetCore.Mvc;

namespace SEP3_T2.Controllers; 

public class ChatController : Controller {
    // GET
    public IActionResult Index() {
        return View();
    }
}