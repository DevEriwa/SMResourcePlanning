using Core.ViewModels;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Resource_Planing.Controllers
{
	public class UserController : Controller
    {
		private IUserHelper _userHelper;
		private IDropdownHelper _dropdownHelper;

		public UserController(IUserHelper userHelper, IDropdownHelper dropdownHelper)
		{
			_userHelper = userHelper;
			_dropdownHelper = dropdownHelper;
		}

		public IActionResult Index()
        {
            return View();
        }

		
	}


    
}
