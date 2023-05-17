using Microsoft.AspNetCore.Mvc;

namespace Resource_Planing.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
