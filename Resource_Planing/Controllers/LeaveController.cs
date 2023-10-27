using Core.Db;
using Core.ViewModels;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLeave(string leaveDetails)
        {
            {
                if (leaveDetails != null)
                {
                    var leaveViewModel = JsonConvert.DeserializeObject<LeaveViewModel>(leaveDetails);
                    if (leaveViewModel != null)
                    {
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


        public IActionResult RequestLeave()
        {
            try
            {
                var loggedInUser = _userHelper.FindByUserName(User.Identity.Name);
                ViewBag.Leave = _dropdownHelper.AllLeaveType(loggedInUser.UserName);
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
