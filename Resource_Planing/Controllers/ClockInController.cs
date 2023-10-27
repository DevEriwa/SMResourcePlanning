using Core.Db;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Resource_Planing.Controllers
{
    public class ClockInController : Controller
    {
        private AppDbContext _context;
        private IUserHelper _userHelper;
        private IRotaHelper _rotaHelper;
        private IDropdownHelper _dropdownHelper;
        public ClockInController(AppDbContext context, IUserHelper userHelper, IRotaHelper rotaHelper, IDropdownHelper dropdownHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _rotaHelper = rotaHelper;
            _dropdownHelper = dropdownHelper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Shifts = _dropdownHelper.GetShifts();
            ViewBag.State = await _dropdownHelper.GetState();
            ViewBag.Country = await _dropdownHelper.GetCountry();
            var shiftLocationList = _userHelper.GetShiftLocationList();
            return View(shiftLocationList);
        }
        [HttpPost]
        public async Task<JsonResult> CreateShiftLocation(string shiftDetails)
        {
            try
            {
                if (shiftDetails != null)
                {
                    var shiftModel = JsonConvert.DeserializeObject<ShiftLocationViewModel>(shiftDetails);
                    if (shiftModel != null)
                    {
                        var shiftLocation = _userHelper.AddShiftLocation(shiftModel);
                        if (shiftLocation)
                        {
                            return Json(new { isError = false, msg = "Shift Location Added successfully" });
                        }
                        return Json(new { isError = true, msg = "Shift Location already exist" });
                    }
                    return Json(new { isError = true, msg = "Unable to Add Shift Location" });
                }
                return Json(new { isError = true, msg = "Network failure, please try again." });
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
		[HttpPost]
		public JsonResult EditShiftLocation(string shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftViewModel = JsonConvert.DeserializeObject<ShiftLocationViewModel>(shiftDetails);
				if (shiftViewModel != null)
				{
					var editTreatment = _userHelper.EditShiftLocation(shiftViewModel);
					if (editTreatment)
					{
						return Json(new { isError = false, msg = "Shift Location Updated successfully", url = "/Treatment/Index" });
					}
				}
				return Json(new { isError = false, msg = "Unable to update Treatment" });
			}
			return Json(new { isError = false, msg = "Network failure, please try again." });
		}
		[HttpPost]
		public JsonResult DeleteShiftLocation(string shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftViewModel = JsonConvert.DeserializeObject<ShiftLocationViewModel>(shiftDetails);
				if (shiftViewModel != null)
				{
					var deleteShift = _userHelper.DeleteShiftLocation(shiftViewModel);
					if (deleteShift)
					{
						return Json(new { isError = false, msg = "Treatment Deleted successfully" });

					}
				}
				return Json(new { isError = true, msg = "Unable to Delete Treatment" });
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}
        public JsonResult GetShiftLocationById(int shiftLocationID)
        {
            if (shiftLocationID != 0)
            {
                var treatment = _userHelper.GetShiftLocation(shiftLocationID);
                if (treatment != null)
                {
                    return Json(new { isError = false, data = treatment });
                }
            }
            return Json(new { isError = true, msg = "No Result Found" });

        }
        [HttpGet]
        public IActionResult AddShiftLocation(int shiftId)
        {
            var shift = _userHelper.GetShiftById(shiftId).Result;
            if (shift != null)
            {
                return PartialView(shift);
            }
            return PartialView();
        }
        [HttpGet]
        public JsonResult AddLocationShift(int shiftId, string latitude, string longitude, string radius)
        {
            try
            {
                if (shiftId < 0 || latitude == null || longitude == null)
                {
                    return Json(new { isError = true, msg = "ensure that all details are entered correctly" });
                }
                var shift = _userHelper.GetShiftById(shiftId).Result;
                if (shift == null)
                {
                    return Json(new { isError = true, msg = "ensure that all details are entered correctly" });
                }
                shift.Latitude = Convert.ToDouble(latitude);
                shift.Longitude = Convert.ToDouble(longitude);
                shift.AcceptedRadius = Convert.ToDouble(radius);
                _context.ShiftsLocations.Update(shift);
                _context.SaveChanges();
                return Json(new { isError = false, msg = "Successfully added location coordinate to this branch" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult ClockInView()
        {
            return View();
        }
    }
}
