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

        public IActionResult RequestLeave()
        {
            try
            {
                var loggedInUser = _userHelper.FindByUserName(User.Identity.Name);
                ViewBag.LoggedInUser = loggedInUser.Id;
                ViewBag.Leave = _dropdownHelper.AllLeaveType(loggedInUser.UserName);
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult StaffRequestLeave(string leaveDetails,string staffId)
        {
            {
                if (leaveDetails != null)
                {
                    var leaveViewModel = JsonConvert.DeserializeObject<RequestLeaveViewModel>(leaveDetails);
                    if (leaveViewModel != null)
                    {
                        var addleave = _leaveApplicationHelper.StaffRequestLeave(leaveViewModel, staffId);
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
    }
}
