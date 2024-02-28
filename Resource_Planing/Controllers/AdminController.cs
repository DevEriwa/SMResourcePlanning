using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Logic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using static Core.Enums.Resource_Planing;

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
        private ILeaveApplicationHelper _leaveApplicationHelper;
        private readonly IEmailServices _emailServices;
        private readonly IEmailHelper _emailHelper;
        public AdminController(AppDbContext context,
            IDropdownHelper dropdownHelper,
            UserManager<ApplicationUser> userManager,
            IUserHelper userHelper, IWebHostEnvironment
            webHostEnvironment, IRotaHelper rotaHelper,
            ILeaveApplicationHelper leaveApplicationHelper,
            IEmailHelper emailHelper, 
			IEmailServices emailServices)
        {
            _context = context;
            _dropdownHelper = dropdownHelper;
            _userManager = userManager;
            _userHelper = userHelper;
            _webHostEnvironment = webHostEnvironment;
            _rotaHelper = rotaHelper;
            _leaveApplicationHelper = leaveApplicationHelper;
            _emailHelper = emailHelper;
            _emailServices = emailServices;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Location()
		{
			var newLocationList = new List<Location>();
			var locations = _userHelper.GetLocations();
			ViewBag.UserInRota = _dropdownHelper.GetAllUsersInRota();
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
						if (freque != null)
						{
							return Json(new { isError = false, data = freque, msg = "Shift Added successfully" });
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
						return Json(new { isError = false, msg = "Shift Updated successfully" });
					}
				}
				return Json(new { isError = true, msg = "Unable to update Shift" });
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}

		[HttpPost]
		public JsonResult DeleteShift(int id)
		{
			if (id != 0)
			{
				var ShiftToBeDeleted = _userHelper.DeleteShift(id);
				if (ShiftToBeDeleted)
				{
					return Json(new { isError = false, msg = "Shift  deleted Successfully" });
				}
				return Json(new { isError = true, msg = "Unable To delete Shift" });
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}

		public JsonResult GetShiftByID(int rotaShiftId)
		{
			if (rotaShiftId != 0)
			{
				var productVaccine = _context.shift.Where(s => s.Id == rotaShiftId).Include(l => l.Locations).FirstOrDefault();
				if (productVaccine != null)
				{
					return Json(productVaccine);
				}
			}
			return null;
		}

		//[HttpGet]
		//public IActionResult AllocateShifts(RotaObjectViewModel rotaData)
		//{
		//	var mod = new StaffRota();
		//	ViewBag.UserInRota = _dropdownHelper.GetAllUsersInRota();
		//	ViewBag.ListOFShifts = _userHelper.GetShifts();
		//	if (rotaData.UserId != null)
		//	{
		//		var model = _rotaHelper.GetWeeklyStaffRota(rotaData.UserId, rotaData.Datee, rotaData.WeekCount);
		//		if(rotaData.Datee == DateTime.MinValue)
		//		{model.DateCreated = DateTime.Now;}
		//		else
		//		{model.DateCreated = rotaData.Datee;}
		//		if (model != null)
		//		{
		//			return View(model);
		//		}
		//	}
		//	mod.ShowAddBTN = "d-none";
		//	return View(mod);
		//}

		//[HttpGet]
		//public JsonResult GetWeeklyUserRota(string rotaData)
		//{
		//	if(rotaData != null)
		//	{
		//		var data = JsonConvert.DeserializeObject<RotaObjectViewModel>(rotaData);
		//		if (data.UserId != null)
		//		{
		//			var model = _rotaHelper.GetWeeklyStaffRota(data.UserId, DateTime.Parse(data.Date), data.WeekCount);
		//			if (model != null)
		//			{
		//				return Json(model);
		//			}
		//		}
		//	}
		//	return null;
		//}

		public JsonResult UpdateRotaDatas(string rotaData)
		{
			if (rotaData != null)
			{
				var data = JsonConvert.DeserializeObject<RotaObjectViewModel>(rotaData);
				if (data.UserId != null)
				{
					var model = _rotaHelper.UpdateRota(data);
					if (model != null)
					{
						//var url = "/Rota/Index/";
						return Json(new { isError = false, msg = "Shift updated succesfully"});
					}
					return Json(new { isError = true, msg = "Unable to update shift" });
				}
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}

        public JsonResult UpdateRotaData(string rotaData)
        {
            if (rotaData != null)
            {
                var data = JsonConvert.DeserializeObject<RotaObjectViewModel>(rotaData);
                if (data.UserId != null)
                {
                    var model = _rotaHelper.UpdateRota(data);
                    if (model != null)
                    {
                        // Generate the updated content for the specific cell
                        var updatedContent = _rotaHelper.GenerateContentForUpdatedRota(data);

                        // Return JSON response with updated content
                        return Json(new { isError = false, msg = "Shift updated successfully", updatedContent });
                    }
                    return Json(new { isError = true, msg = "Unable to update shift" });
                }
            }
            return Json(new { isError = true, msg = "Network failure, please try again." });
        }

        [HttpPost]
		public ActionResult UpdateLocation(int locationId, double latitude, double longitude, double acceptedRadius)
		{
			try
			{
				var locationUpdate = _rotaHelper.UpdateLocation(locationId, latitude, longitude, acceptedRadius);
				if (locationUpdate)
				{
					var url = "/Admin/Location/";
					return Json(new { isError = false, msg = "ClockIn location updated succesfully", url = url });
				}
				return Json(new { isError = true, msg = "Unable to update" });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, errorMessage = ex.Message });
			}

		}


        [HttpGet]
		public IActionResult Leave()
		{
			var loggedInUser = _userHelper.FindByUserName(User.Identity.Name);
			if (loggedInUser != null)
			{
				ViewBag.LeaveType = _dropdownHelper.AllLeaveType(loggedInUser.UserName);
                ViewBag.LeaveStatus = _dropdownHelper.GetLeaveStatus();
				if (loggedInUser != null)
				{
					ViewBag.Leave = _dropdownHelper.AllLeaveType(loggedInUser.UserName);
					return View();
				}
				else
				{
					return RedirectToAction("CantAccess", "Home");
				}
			}
			return null;
		}



		[HttpGet]
        public async Task<IActionResult> LeaveType()
        {
            ViewBag.Shift = await _dropdownHelper.GetStaffShifts();
            return View();
        }

        [HttpPost]
        public IActionResult CreateLeaveType(string leaveDetails)
        {
            {
                if (leaveDetails != null)
                {
                    var leaveViewModel = JsonConvert.DeserializeObject<LeaveViewModel>(leaveDetails);
                    if (leaveViewModel != null)
                    {
                        if (leaveViewModel.ShiftId == null)
                        {
                            return Json(new { isError = true, msg = "No leave set for this shift" }); ;
                        };

                        if (leaveViewModel.Name == null && (leaveViewModel.NumberOfDays == 0))
                        {
                            return Json(new { isError = true, msg = "Unable To Add Leave" });
                        }

                        if (leaveViewModel.Name == null && leaveViewModel.NumberOfDays > 0)
                        {
                            leaveViewModel.Name = "Annual Leave";
                        };

                        if (leaveViewModel.Name == null)
                        {
                            return Json(new { isError = true, msg = "Unable To Add Leave" });
                        };
                        if (leaveViewModel.Name.ToLower() != "annual leave" && leaveViewModel.NumberOfDays > 0)
                        {
                            leaveViewModel.NumberOfDays = 0;
                        }
                        if (leaveViewModel.NumberOfDays < 0)
                        {
                            return Json(new { isError = true, msg = " Number of days cant be less than 1" });
                        };
                        var existingLeave = _context.LeaveSetups.Where(s => s.Name == leaveViewModel.Name && s.ShiftId == leaveViewModel.ShiftId && !s.Deleted).FirstOrDefault();
                        if (existingLeave != null)
                        {
                            return Json(new { isError = true, msg = "A leave with similar Name already exist.\n Please, choose a different name" });
                        }
                        var addleave = _leaveApplicationHelper.CreateLeave(leaveViewModel);
                        if (addleave)
                        {
                            return Json(new { isError = false, msg = "Leave Added successfully" });
                        }
                        return Json(new { isError = true, msg = "Unable To Add Leave" });
                    }
                    return Json(new { isError = true, msg = "Error Occurred" });
                }
                return Json(new { isError = true, msg = "Error Occurred" });
            }
        }


        public IActionResult ViewLeaveReason(int leaveId)
        {
			var leave = _userHelper.GetLeaveById(leaveId);
			if (leave != null)
			{
				return PartialView(leave);
			}
			return null;
		}
        [HttpPost]
        public IActionResult ApproveLeave(int id)
        {
            try
            {
                var employeeLeave = _context.LeaveApplications.Where(x => x.Id == id).Include(x => x.User).FirstOrDefault();
                if (employeeLeave != null)
                {
                    employeeLeave.Status = LeaveStatus.Approved;
                    employeeLeave.DateApproved = DateTime.Now;
                    _context.LeaveApplications.Update(employeeLeave);
                    _context.SaveChanges();

                    string receiverAddress = employeeLeave.User.Email;
                    string subject = "Hr Support Leave Approval";
                    string messageBody = "Hello " + employeeLeave.User.Name + "<br/> Your Leave Application has been approved to start from " + employeeLeave.StartDate.ToString("MM-dd-yy") + " to "
                                          + employeeLeave.EndDate.ToString("MM-dd-yy") + "<br/> We hope you come back stronger! <br/> Kind Regards <br/> Hr Team";
                      _emailServices.SendEmail(receiverAddress, subject, messageBody);

                   // _emailHelper.SendLeaveApprovalEmailToDepartmentStaff(employeeLeave.User, employeeLeave);

                    ModelState.Clear();
                    return RedirectToAction("Leave");
                }
                return RedirectToAction("Leave");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        [HttpPost]
        public IActionResult DeclineLeave(int? id)
        {
            var employeeLeave = _context.LeaveApplications.Where<LeaveApplication>(x => x.Id == id).Include(x => x.User).FirstOrDefault();
            employeeLeave.Status = LeaveStatus.Declined;
            _context.LeaveApplications.Update(employeeLeave);
            _context.SaveChanges();
            string ADtoAddress = employeeLeave.User.Email;
            var ADsubject = "Resouce Planning Support Leave Decline";
            string ADMessages = "Hello " + employeeLeave.User.Name + "<br/> Your Leave Request can not be approved at this moment. " +
                                "<br/> Kindly meet your <b>Line manager<b/> for more information ";
            _emailServices.SendEmail(ADtoAddress, ADsubject, ADMessages);
            ModelState.Clear();
            return RedirectToAction("Leave");
        }


        [HttpGet]
        public JsonResult EditLeaveType(int id)
        {
            ViewBag.Leavetype = _dropdownHelper.GetStaffShiftDropDown(User.Identity.Name).Result;
            if (id != 0)
            {
                var leaveType = _leaveApplicationHelper.GetLeaveTypeById(id).Result;
                if (leaveType != null)
                {
                    return Json(leaveType);
                }
                return Json(new { isError = true, msg = "Error occured" });
            }
            return Json(new { isError = true, msg = "Error Occured" });
        }

        [HttpPost]
        public JsonResult EditedLeaveType(string leaveTypeDetails)
        {
            if (leaveTypeDetails != null)
            {
                var leaveTypeViewModel = JsonConvert.DeserializeObject<LeaveTypeViewModel>(leaveTypeDetails);
                if (leaveTypeViewModel != null)
                {
                    var editedLeaveType = _leaveApplicationHelper.EditLeaveType(leaveTypeViewModel);
                    if (editedLeaveType != null)
                    {
                        return Json(new { isError = false, msg = "LeaveType Edited Successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable To Edit LeaveType" });
                }
                return Json(new { isError = true, msg = "Error occured" });
            }
            return Json(new { isError = true, msg = "Error Occured" });
        }

        [HttpPost]
        public JsonResult LeaveTypeToBeDeleted(int id)
        {
            if (id != 0)
            {
                var deleteLeaveType = _leaveApplicationHelper.DeleteLeaveType(id);
                if (deleteLeaveType != null)
                {
                    return Json(new { isError = false, msg = "LeaveType  deleted Successfully" });
                }
                return Json(new { isError = true, msg = "Unable To delete LeaveType" });
            }
            return Json(new { isError = true, msg = "Error occured" });
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

        [HttpGet]
        public JsonResult FetchShifts(string date, string userId, string year)
        {
            // Assuming your StaffRota model has a property named RotaObjectString for storing shift data
            var staffRota = _context.StaffRotas
                .Where(sr => sr.UserId == userId && sr.Year == year)
                .FirstOrDefault();

            if (staffRota != null)
            {
                var shifts = staffRota.RotaObjectGet
                    .Where(ro => ro.Date == date)
                    .ToArray();

                return Json(shifts);
            }

            return Json(new RotaObject[0]);  // Return an empty array if no shifts found
        }

    }
}

