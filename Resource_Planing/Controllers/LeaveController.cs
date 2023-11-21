using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using static Core.Enums.Resource_Planing;

namespace Resource_Planing.Controllers
{
    public class LeaveController : Controller
    {
        private readonly AppDbContext _context;
        private ILeaveApplicationHelper _leaveApplicationHelper;
        private IUserHelper _userHelper;
        private IDropdownHelper _dropdownHelper;
        public LeaveController(ILeaveApplicationHelper leaveApplicationHelper, AppDbContext context, IUserHelper userHelper, IDropdownHelper dropdownHelper)
        {
            _context = context;
            _leaveApplicationHelper=leaveApplicationHelper;
            _userHelper=userHelper;
            _dropdownHelper=dropdownHelper;
        }

        public IActionResult RequestLeave()
        {
                var loggedInUser = _userHelper.FindByUserName(User.Identity.Name);
                ViewBag.LoggedInUser = loggedInUser.Id;
            var dghdgfghjghj = _leaveApplicationHelper.GetListOfAllLeaveApplication(loggedInUser.Id);
                var staffLeave = GetRemainingLeave();
                if (staffLeave != null)
                {
                    ViewData["StaffLeave"] = staffLeave;
                }
                ViewBag.Leave = _dropdownHelper.AllLeaveType(loggedInUser.UserName);
                return View();
        }


        [HttpPost]
		//public IActionResult StaffRequestLeave(string leaveDetails, string staffId)
		//{
		//    if (leaveDetails != null)
		//    {
		//        var leaveViewModel = JsonConvert.DeserializeObject<RequestLeaveViewModel>(leaveDetails);
		//        if (leaveViewModel != null)
		//        {
		//            var userName = User.Identity.Name;
		//            ViewData["StaffLeave"] = GetRemainingLeave();
		//            var currentUser = _userHelper.FindByUserName(userName);

		//            if (currentUser != null)
		//            {
		//                var annualLeave = _userHelper.GetAnnualLeave(userName);

		//                if (annualLeave == null)
		//                {
		//                    return Json(new { isError = true, msg = "No Annual Leave created for your branch" });
		//                }
		//                else
		//                {
		//                    if (Convert.ToDecimal(leaveViewModel.RemainingLeaveDays) < Convert.ToDecimal(leaveViewModel.NumberOfDaysRemaining))
		//                    {
		//                        return Json(new { isError = true, msg = "Remaining number of days for this leave has Exceeded!" });
		//                    }

		//                    if (leaveViewModel.LeaveTypeId == annualLeave.Id)
		//                    {
		//                        leaveViewModel.LeaveTypeName = "Annual Leave";
		//                    }

		//                    var addleave = _leaveApplicationHelper.StaffRequestLeave(leaveViewModel, staffId);

		//                    if (addleave)
		//                    {
		//                        return Json(new { isError = false, msg = "Leave Added successfully" });
		//                    }

		//                    return Json(new { isError = true, msg = "Unable To Add Leave" });
		//                }
		//            }
		//        }

		//        return Json(new { isError = true, msg = "Error Occurred" });
		//    }

		//    return Json(new { isError = true, msg = "Error Occurred" });
		//}

		public IActionResult StaffRequestLeave(string leaveDetails, string staffId)
		{
			try
			{
				if (string.IsNullOrEmpty(leaveDetails) || string.IsNullOrEmpty(staffId))
				{
					return BadRequest(new { isError = true, msg = "Invalid input parameters" });
				}

				var leaveViewModel = JsonConvert.DeserializeObject<RequestLeaveViewModel>(leaveDetails);

				if (leaveViewModel == null)
				{
					return BadRequest(new { isError = true, msg = "Invalid leave details" });
				}

				var userName = User.Identity?.Name;

				if (string.IsNullOrEmpty(userName))
				{
					return Unauthorized(new { isError = true, msg = "User not authenticated" });
				}

				ViewData["StaffLeave"] = GetRemainingLeave();
				var currentUser = _userHelper.FindByUserName(userName);

				if (currentUser == null)
				{
					return Json(new { isError = true, msg = "User not found" });
				}

				var annualLeave = _userHelper.GetAnnualLeave(userName);

				if (annualLeave == null)
				{
					return Json(new { isError = true, msg = "No Annual Leave created for your branch" });
				}

				if (Convert.ToDecimal(leaveViewModel.RemainingLeaveDays) < Convert.ToDecimal(leaveViewModel.NumberOfDaysRemaining))
				{
					return Json(new { isError = true, msg = "Remaining number of days for this leave has exceeded" });
				}

				if (leaveViewModel.LeaveTypeId == annualLeave.Id)
				{
					leaveViewModel.LeaveTypeName = "Annual Leave";
				}

				var addleave = _leaveApplicationHelper.StaffRequestLeave(leaveViewModel, staffId);

				if (addleave)
				{
					return Json(new { isError = false, msg = "Leave Added successfully" });
				}

				return Json(new { isError = true, msg = "Unable To Add Leave" });
			}
			catch (Exception ex)
			{
				// Log the exception for troubleshooting
				// Consider returning a more detailed error message for debugging purposes
				return StatusCode(500, new { isError = true, msg = "Internal server error" });
			}
		}



		public decimal GetRemainingLeave()
        {
            var staff = _userHelper.FindAdminByUserName(User.Identity.Name);
            if (staff != null)
            {
                var employeeLeave = _context.LeaveApplications
                    .OrderBy(a => a.StartDate)
                    .Where(x => x.StaffId == staff.Id
                        && x.StartDate.Year == DateTime.Now.Year
                        && (x.Status == LeaveStatus.Approved || x.Status == LeaveStatus.Absence)
                        && x.Active
                        && !x.Deleted)
                    .LastOrDefault();
                if (employeeLeave != null)
                {
                    return employeeLeave.RemainingLeave;
                }
            }
            var CompanyUpdatedLeaveDays = _context.LeaveSetups
                .Where(x => x.Active && !x.Deleted)
                .Sum(d => d.NumberOfDays);
            return CompanyUpdatedLeaveDays;
        }


        [HttpPost]
        public JsonResult CancelLeave(int id)
        {
            if (id != 0)
            {
                var employeeLeave = _context.LeaveApplications.Where(x => x.Id == id).FirstOrDefault();
                if (employeeLeave != null)
                {
                    employeeLeave.Status = LeaveStatus.Cancel;
                    _context.LeaveApplications.Update(employeeLeave);
                    _context.SaveChanges();
                    return Json(new { isError = false, msg = "Leave Cancel Successfully." });
                }
                return Json(new { isError = true, msg = "Unable To Cancel leave" });
            }
            return Json(new { isError = true, msg = "Error occured" });

        }

        [HttpGet]
        public JsonResult EditStaffLeave(int id)
        {
            if (id != 0)
            {
                var staffLeave = _leaveApplicationHelper.GetEmployeeLeaveById(id);
                if (staffLeave != null)
                {
                    return Json(staffLeave);
                }
                return Json(new { isError = true, msg = "Error occured" });
            }
            return Json(new { isError = true, msg = "Error Occured" });
        }

        [HttpPost]
        public JsonResult EditStaffLeave(string leaveDetails)
        {
            if (leaveDetails != null)
            {
                var employeeLeaveViewModel = JsonConvert.DeserializeObject<RequestLeaveViewModel>(leaveDetails);
                if (employeeLeaveViewModel != null)
                {
                    var employeeLeave = _leaveApplicationHelper.EditEmployeeLeave(employeeLeaveViewModel);
                    if (employeeLeave != null)
                    {
                        return Json(new { isError = false, msg = "Leave Edited Successfully" });
                    }
                    return Json(new { isError = true, msg = "Unable To Edit Leavel" });
                }
                return Json(new { isError = true, msg = "Error occured" });
            }
            return Json(new { isError = true, msg = "Error Occured" });
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
    }
}
