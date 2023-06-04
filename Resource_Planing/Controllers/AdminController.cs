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

		private UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public AdminController(AppDbContext context, IDropdownHelper dropdownHelper,UserManager<ApplicationUser> userManager, IUserHelper userHelper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_dropdownHelper = dropdownHelper;
			_userManager = userManager;
			_userHelper = userHelper;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
        {
            return View();
        }

		public IActionResult Location()
		{
			var currentUser = _userHelper.FindByUserName(User?.Identity?.Name);
			if (currentUser != null)
			{
				var locations = _userHelper.GetLocations(currentUser.UserName);
				return View(locations);
			}
			return View();
		}

		[HttpPost]
		public JsonResult AddLocations(string locationDetails)
		{
			if (locationDetails != null)
			{
				var locationViewModel = JsonConvert.DeserializeObject<LocationViewModel>(locationDetails);
				if (locationViewModel != null)
				{
					var currentUser = User?.Identity?.Name;
					if (currentUser != null)
					{
						var addlocation = _userHelper.AddLoction(locationViewModel, currentUser);
						if (addlocation)
						{
							return Json(new { isError = false, msg = "Location Added successfully" });
						}
						return Json(new { isError = true, msg = "Unable To Add Location" });
					}
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
				var currentUser = User?.Identity?.Name;
				if (currentUser != null)
				{
					var locationToBeEdited = _userHelper.GetLocationById(id, currentUser);
					if (locationToBeEdited != null)
					{
						return Json(locationToBeEdited);
					}
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
					var currentUser = User?.Identity?.Name;
					if (currentUser != null)
					{
						var editedLocation = _userHelper.LocationEdited(locationViewModel, currentUser);
						if (editedLocation)
						{
							return Json(new { isError = false, msg = "Location Updated Successfully" });
						}
						return Json(new { isError = true, msg = "Unable To Updated Location" });
					}
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
            var currentUser = _userHelper.FindByUserName(User?.Identity?.Name);
            if (currentUser != null)
            {
                var departments = _userHelper.GetDEepartments(currentUser.UserName);
                return View(departments);
            }
            return View();
        }

        [HttpPost]
        public JsonResult AddDepartments(string departmentDetails)
        {
            if (departmentDetails != null)
            {
                var departmentViewModel = JsonConvert.DeserializeObject<DepartmentViewModel>(departmentDetails);
                if (departmentViewModel != null)
                {
                    var currentUser = User?.Identity?.Name;
                    if (currentUser != null)
                    {
                        var adddepartment = _userHelper.AddDepartment(departmentViewModel, currentUser);
                        if (adddepartment)
                        {
                            return Json(new { isError = false, msg = "Department Added successfully" });
                        }
                        return Json(new { isError = true, msg = "Unable To Add Department" });
                    }
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
                var currentUser = User?.Identity?.Name;
                if (currentUser != null)
                {
                    var departmentToBeEdited = _userHelper.GetLocationById(id, currentUser);
                    if (departmentToBeEdited != null)
                    {
                        return Json(departmentToBeEdited);
                    }
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
                    var currentUser = User?.Identity?.Name;
                    if (currentUser != null)
                    {
                        var editedDepartment = _userHelper.DepartmentEdited(departmentViewModel, currentUser);
                        if (editedDepartment)
                        {
                            return Json(new { isError = false, msg = "Department Updated Successfully" });
                        }
                        return Json(new { isError = true, msg = "Unable To Updated Department" });
                    }
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


    }
}
