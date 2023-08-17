using Core.Db;
using Core.ViewModels;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;

namespace Resource_Planing.Controllers
{
    public class RotaController : Controller
    {
		private AppDbContext _context;
		private IUserHelper _userHelper;
		private IRotaHelper _rotaHelper;
		private IDropdownHelper _dropdownHelper;

		public RotaController(AppDbContext context, IUserHelper userHelper, IRotaHelper rotaHelper, IDropdownHelper dropdownHelper)
		{
			_context = context;
			_userHelper = userHelper;
			_rotaHelper = rotaHelper;
			_dropdownHelper = dropdownHelper;
		}



		public IActionResult Index()
        {
			ViewBag.ListOFShifts = _userHelper.GetShifts();
			ViewBag.Location = _dropdownHelper.GetLocations();
            return View();
        }

		public JsonResult GetRotaDataByDateRange(DateTime startDate, DateTime endDate)
		{
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                var model = _rotaHelper.GenerateNewRota(startDate, endDate);
                if (model != null)
                {
                    return Json(model);
                }
            }
            return null;
        }
    }
}
