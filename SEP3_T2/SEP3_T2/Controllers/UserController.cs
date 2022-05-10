using Microsoft.AspNetCore.Mvc;

namespace SEP3_T2.Controllers; 

public class UserController : Controller {
    [Route("")]
    public IActionResult Index() {
        return View();
    }
}