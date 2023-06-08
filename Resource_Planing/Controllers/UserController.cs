using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public IActionResult Shift()
        {
            return View();
        }

		[HttpPost]
		public JsonResult AddShift(string shiftDetails)
		{
			try
			{
				if (shiftDetails != null)
				{
					var shiftModel = JsonConvert.DeserializeObject<DepartmentViewModel>(shiftDetails);
					if (shiftModel != null)
					{
						var freque = _userHelper.AddShift(shiftModel);
						if (freque)
						{
							return Json(new { isError = false, msg = "Shift Added successfully" });
						}
						return Json(new { isError = true, msg = "Shift already exist" });
					}
					return Json(new { isError = true, msg = "Unable to Add Shift" });
				}
				return Json(new { isError = true, msg = "Network failure, please try again." });
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
	}


    
}
