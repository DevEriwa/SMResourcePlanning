using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Resource_Planing.Controllers
{
    public class AdminController : Controller
    {
		private AppDbContext _context;
		private IDropdownHelper _dropdownHelper;
		private IUserHelper _userHelper;
		private IRotaHelper _rotaHelper;
		private UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public AdminController(AppDbContext context, IDropdownHelper dropdownHelper, UserManager<ApplicationUser> userManager, IUserHelper userHelper, IWebHostEnvironment webHostEnvironment, IRotaHelper rotaHelper)
		{
			_context = context;
			_dropdownHelper = dropdownHelper;
			_userManager = userManager;
			_userHelper = userHelper;
			_webHostEnvironment = webHostEnvironment;
			_rotaHelper = rotaHelper;
		}
		public IActionResult Index()
        {
            return View();
        }

		public IActionResult Location()
		{
			var newLocationList = new List<Location>();
			var locations = _userHelper.GetLocations();
			if (locations.Any())
			{
				newLocationList = locations;
			}
			return View(newLocationList);
		}

		[HttpPost]
		public JsonResult AddLocations(string locationDetails)
		{
			if (locationDetails != null)
			{
				var locationViewModel = JsonConvert.DeserializeObject<LocationViewModel>(locationDetails);
				if (locationViewModel != null)
				{
					var addlocation = _userHelper.AddLoction(locationViewModel);
					if (addlocation)
					{
						return Json(new { isError = false, msg = "Location Added successfully" });
					}
					return Json(new { isError = true, msg = "Unable To Add Location" });
				}
				return Json(new { isError = true, msg = "Error Occurred" });
			}
			return Json(new { isError = true, msg = "Error Occurred" });
		}

		[HttpGet]
		public JsonResult EditLocation(int id)
		{
			if (id > 0)
			{
				var locationToBeEdited = _userHelper.GetLocationById(id);
				if (locationToBeEdited != null)
				{
					return Json(locationToBeEdited);
				}
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}
		[HttpPost]
		public JsonResult EditedLocation(string locationdetails)
		{
			if (locationdetails != null)
			{
				var locationViewModel = JsonConvert.DeserializeObject<LocationViewModel>(locationdetails);
				if (locationViewModel != null)
				{
					var editedLocation = _userHelper.LocationEdited(locationViewModel);
					if (editedLocation)
					{
						return Json(new { isError = false, msg = "Location Updated Successfully" });
					}
					return Json(new { isError = true, msg = "Unable To Updated Location" });
				}
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}

		[HttpPost]
		public JsonResult DeleteLocation(int id)
		{
			if (id != 0)
			{
				var locationToBeDeleted = _userHelper.DeleteLocation(id);
				if (locationToBeDeleted)
				{
					return Json(new { isError = false, msg = "Location  deleted Successfully" });
				}
				return Json(new { isError = true, msg = "Unable To delete Location" });
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}

        public IActionResult Department()
        {
			var dept = new List<Department>();
			var departments = _userHelper.GetListOfAllDepartment();
			if (departments.Any())
			{
				dept = departments;
			}
			return View(departments);
        }

        [HttpPost]
        public JsonResult AddDepartments(string departmentDetails)
        {
            if (departmentDetails != null)
            {
                var departmentViewModel = JsonConvert.DeserializeObject<DepartmentViewModel>(departmentDetails);
                if (departmentViewModel != null)
                {
					var adddepartment = _userHelper.AddDepartment(departmentViewModel);
					if (adddepartment)
					{
						return Json(new { isError = false, msg = "Department Added successfully" });
					}
					return Json(new { isError = true, msg = "Unable To Add Department" });
				}
                return Json(new { isError = true, msg = "Error Occurred" });
            }
            return Json(new { isError = true, msg = "Error Occurred" });
        }

        [HttpGet]
        public JsonResult EditDepartment(int id)
        {
            if (id > 0)
            {
				var departmentToBeEdited = _userHelper.GetLocationById(id);
				if (departmentToBeEdited != null)
				{
					return Json(departmentToBeEdited);
				}
			}
            return Json(new { isError = true, msg = "Network failure, please try again." });
        }

        [HttpPost]
        public JsonResult EditedDepartment(string departmentdetails)
        {
            if (departmentdetails != null)
            {
                var departmentViewModel = JsonConvert.DeserializeObject<DepartmentViewModel>(departmentdetails);
                if (departmentViewModel != null)
                {
					var editedDepartment = _userHelper.DepartmentEdited(departmentViewModel);
					if (editedDepartment)
					{
						return Json(new { isError = false, msg = "Department Updated Successfully" });
					}
					return Json(new { isError = true, msg = "Unable To Updated Department" });
				}
            }
            return Json(new { isError = true, msg = "Network failure, please try again." });
        }

        [HttpPost]
        public JsonResult DeleteDepartment(int id)
        {
            if (id != 0)
            {
                var departmentToBeDeleted = _userHelper.DeleteDepartment(id);
                if (departmentToBeDeleted)
                {
                    return Json(new { isError = false, msg = "Department  deleted Successfully" });
                }
                return Json(new { isError = true, msg = "Unable To delete Department" });
            }
            return Json(new { isError = true, msg = "Network failure, please try again." });
        }

		public IActionResult Shift()
		{
			var rotaShift = new List<Shifts>();
			ViewBag.Location = _dropdownHelper.GetLocations();
			var shifts = _userHelper.GetShifts();
			if (shifts.Any())
			{
				rotaShift = shifts;
			}
			return View(rotaShift);
		}

		[HttpPost]
		public JsonResult AddShift(string shiftDetails)
		{
			try
			{
				if (shiftDetails != null)
				{
					var shiftModel = JsonConvert.DeserializeObject<ShiftViwModel>(shiftDetails);
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


		[HttpPost]
		public JsonResult EditShift(string shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftViewModel = JsonConvert.DeserializeObject<ShiftViwModel>(shiftDetails);
				if (shiftViewModel != null)
				{
					var createProductVaccine = _userHelper.EditShift(shiftViewModel);
					if (createProductVaccine)
					{
						return Json(new { isError = false, msg = "ProductVaccine Updated successfully" });
					}
				}
				return Json(new { isError = true, msg = "Unable to update ProductVaccine" });
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}

		public JsonResult GetShiftByID(int rotaShiftId)
		{
			if (rotaShiftId != 0)
			{
				var productVaccine = _context.shift.Where(c => c.Id == rotaShiftId).FirstOrDefault();
				if (productVaccine != null)
				{
					return Json(productVaccine);
				}
			}
			return null;
		}

		[HttpGet]
		public IActionResult AllocateShifts(RotaObjectViewModel rotaData)
		{
			var mod = new StaffRota();
			if (rotaData.UserId != null)
			{
				var model = _rotaHelper.GetWeeklyStaffRota(rotaData.UserId, rotaData.Datee, rotaData.WeekCount);
				if(rotaData.Datee == DateTime.MinValue)
				{model.DateCreated = DateTime.Now;}
				else
				{model.DateCreated = rotaData.Datee;}
				if (model != null)
				{
					return View(model);
				}
			}
			mod.ShowAddBTN = "d-none";
			return View(mod);
		}

		[HttpGet]
		public JsonResult GetWeeklyUserRota(string rotaData)
		{
			if(rotaData != null)
			{
				var data = JsonConvert.DeserializeObject<RotaObjectViewModel>(rotaData);
				if (data.UserId != null)
				{
					var model = _rotaHelper.GetWeeklyStaffRota(data.UserId, data.Datee, data.WeekCount);
					if (model != null)
					{
						return Json(model);
					}
				}
			}
			return null;
		}
	}
}
