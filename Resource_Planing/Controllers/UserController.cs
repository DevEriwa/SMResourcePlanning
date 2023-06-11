using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Resource_Planing.Controllers
{
	public class UserController : Controller
    {
		private IUserHelper _userHelper;

		public UserController(IUserHelper userHelper)
		{
			_userHelper = userHelper;
		}

		public IActionResult Index()
        {
            return View();
        }

	}


    
}
