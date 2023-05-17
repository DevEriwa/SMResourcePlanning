using Microsoft.AspNetCore.Mvc;

namespace Resource_Planing.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
