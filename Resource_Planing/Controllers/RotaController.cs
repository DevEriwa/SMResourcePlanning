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

		public JsonResult GetRotaDataByDateRange(DateTime startDate, DateTime endDate, int locId)
		{
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue && locId > 0)
            {
                var model = _rotaHelper.GenerateNewRota(startDate, endDate, locId);
                if (model != null)
                {
                    return Json(model);
                }
            }
            return null;
        }

        public JsonResult PunchIn(PunchingViewModel model)
        {
            if (model != null)
            {
                var data = _userHelper.PunchInService(model);
                return Json(new { isError = data.isError, msg = data.Msg });
            }
            return Json(new { isError = false, msg = "Error occurred while trying to process your request" });
        }

        public JsonResult PunchOut(PunchingViewModel model)
        {
            if (model != null)
            {
                var data = _userHelper.PunchOutService(model);
                return Json(new { isError = data.isError, msg = data.Msg });
            }
            return Json(new { isError = false, msg = "Error occurred while trying to process your request" });
        }
    }
}
