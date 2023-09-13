using Core.Db;
using Core.Models;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Resource_Planing;
using static Logic.Helpers.LeaveHelper;

namespace Logic.Helpers
{
    public class LeaveHelper : ILeaveHelper
    {

        private readonly IUserHelper _userHelper;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDropdownHelper _dropdownHelper;

        public LeaveHelper(AppDbContext context, UserManager<ApplicationUser> userManager, IUserHelper userHelper, IDropdownHelper dropdownHelper)
        {
            _context = context;
            _userManager = userManager;
            _userHelper = userHelper;
            _dropdownHelper = dropdownHelper;
        }



        public async Task<List<EmployeeLeave>> GetListOfEmplyeeLeaveApplicationsData()
        {
            var leaves = new List<EmployeeLeave>();
            var leaveApplications = _context.EmployeeLeaves.Include(u => u.User).OrderByDescending(d => d.DeteCreated).ToList();
            if (leaveApplications.Any())
            {
                leaves = leaveApplications;
            }
            return leaves;



        }

		public List<EmployeeLeave> GetListOfAllLeave(string staff)
		{
			var leaves = new List<EmployeeLeave>();
			var leavesApplied = _context.EmployeeLeaves.Where(x =>x.Id > 0 && x.Name == staff && leaves.Count() > 0).ToList();
			if (leavesApplied != null)
			{
				leaves = leavesApplied;
			}
			return leaves;
		}








		// Other methods related to leave service...

		//public async Task<List<EmployeeLeave>> ListOfAllEmployeesAppliedLeave(string staffId)
		//{
		//    var employeeLeave = new List<EmployeeLeave>();
		//    try
		//    {
		//        var loggedInUser = _userManager.Users.Where(s => s.Id == staffId).FirstOrDefault();
		//        var employeesAppliedLeave = await _context.EmployeeLeave.Where(a => a.LeaveStatus == LeaveStatus.Applied && a.CompanyId == loggedInUser.CompanyId && a.StartDate.Year == DateTime.Now.Year).Include(a => a.User).Include(a => a.Leave).ToListAsync();
		//        if (employeesAppliedLeave.Any())
		//        {
		//            return employeesAppliedLeave;
		//        }
		//        return employeeLeave;
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//}
	}
}
