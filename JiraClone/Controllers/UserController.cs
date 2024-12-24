using Microsoft.AspNetCore.Mvc;

namespace JiraClone.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
