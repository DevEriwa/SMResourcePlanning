using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Core.Enums.Resource_Planing;

namespace Resource_Planing.Controllers
{
	[Authorize]
	public class LeaveController : Controller
	{
		private readonly IRotaHelper _rotaHelper;
		private readonly IAccountHelper _accountHelper;
		private readonly IUserHelper _userHelper;
		private readonly IDropdownHelper _dropdownHelper;
		private readonly AppDbContext _context;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public LeaveController(IAccountHelper accountHelper, IUserHelper userHelper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext context, IDropdownHelper dropdownHelper, IRotaHelper rotaHelper)
		{
			_accountHelper = accountHelper;
			_userHelper = userHelper;
			_signInManager = signInManager;
			_userManager = userManager;
			_context = context;
			_dropdownHelper = dropdownHelper;
			_rotaHelper = rotaHelper;
		}
		//public IActionResult Index()
		//{
		//	return View();
		//}
		public IActionResult RequestLeave()
		{
			try
			{
				var loggedInUser = _userHelper.FindByUserName(User.Identity.Name);

					//ViewData["CompanyLeave"] = GetRemainingLeave();
					ViewBag.Leave = _dropdownHelper.GetLeaveTypeDropDown(loggedInUser.UserName);
					return View();

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

        //public decimal GetRemainingLeave()
        //{
        //    var staff = _userHelper.FindByUserName(User.Identity.Name);
        //    var employeeLeave = _context.EmployeeLeaves.OrderBy(a => a.Id).Where<EmployeeLeave>(x => x.StaffName == staff.Id
        //    && x.StartDate.Year == DateTime.Now.Year && (x.LeaveStatus == LeaveStatus.Approved || x.LeaveStatus == LeaveStatus.Absence) && x.Active && !x.Deleted).LastOrDefault();
        //    if (employeeLeave != null)
        //    {
        //        return employeeLeave.RemainingLeave;
        //    }
        //    else
        //    {
        //        var CompanyUpdatedLeaveDays = _context.Leave.Where(x => x.Name == staff.Name
        //               && x.Active && !x.Deleted).Sum(d => d.NumberOfDays);
        //        return CompanyUpdatedLeaveDays.Value;
        //    }
        //}

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
        public JsonResult CancelLeave(int id)
        {
            if (id != 0)
            {
                var employeeLeave = _context.EmployeeLeaves.Where(x => x.Id == id).FirstOrDefault();
                if (employeeLeave != null)
                {
                    employeeLeave.LeaveStatus = LeaveStatus.Cancel;
                    _context.EmployeeLeaves.Update(employeeLeave);
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
                var staffLeave = _userHelper.GetEmployeeLeaveById(id);
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
                var employeeLeaveViewModel = JsonConvert.DeserializeObject<EmployeeLeaveViewModel>(leaveDetails);
                if (employeeLeaveViewModel != null)
                {
                    var employeeLeave = _userHelper.EditEmployeeLeave(employeeLeaveViewModel);
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


    }
}
