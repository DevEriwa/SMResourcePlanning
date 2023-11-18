using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Resource_Planing;

namespace Logic.Helpers
{
    public class LeaveApplicationHelper : ILeaveApplicationHelper
    {

        private readonly AppDbContext _context;
        private readonly IUserHelper _userHelper;
        private UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LeaveApplicationHelper
        (AppDbContext context,
        IUserHelper userHelper,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userHelper = userHelper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public bool CreateLeave(LeaveViewModel leaveDetails)
        {
            if (leaveDetails != null)
            {
                var leaveModel = new LeaveSetup()
                {
                    Id = leaveDetails.Id,
                    Name = leaveDetails.Name,
                    Abbreviations = leaveDetails.Abbreviations,
                    DeductFromAnnualLeave = leaveDetails.DeductFromAnnualLeave,
                    HoursDeductedFromTimesheet = leaveDetails.HoursDeductedFromTimesheet,
                    ShiftId = leaveDetails.ShiftId,
                    NumberOfDays = leaveDetails.NumberOfDays,
                    Active = true,
                    Deleted = false,
                    DeteCreated = DateTime.Now,
                };
                _context.LeaveSetups.Add(leaveModel);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


		public List<RequestLeaveViewModel> GetListOfAllLeaveApplication(string staffId)
		{
			var departments = new List<RequestLeaveViewModel>();

			var departmentToBeEdited = _context.LeaveApplications
				.Where(x => x.Active && !x.Deleted && x.StaffId == staffId)
				.Select(x => new RequestLeaveViewModel
				{
					StaffId = x.StaffId,
					NumberOfDays = x.NumberOfDays,
					RemainingLeave = x.RemainingLeave,
					LeaveReason = x.Reason,
					StartDate = x.StartDate,
					EndDate = x.EndDate,
					LeaveStatus = (LeaveStatus)x.Status,
					RemainingLeaveDays = x.User.FaceImageData,
				})
				.ToList();

			if (departmentToBeEdited != null)
			{
				departments = departmentToBeEdited;
			}

			return departments;
		}



		public bool StaffRequestLeave(RequestLeaveViewModel leaveDetails, string staffId)
        {
            if (leaveDetails != null)
            {
                if (string.IsNullOrEmpty(staffId) || leaveDetails == null)
                {
                    return false;
                }
                var existingLeaveApplication = _context.LeaveApplications.Include(b=> b.Leave)
                    .FirstOrDefault(x => x.Leave.Name == leaveDetails.leaveTypeName);
                if (existingLeaveApplication != null)
                {
                    return false;
                }
                var leaveModel = new LeaveApplication()
                {
                    NumberOfDays = Convert.ToDecimal(leaveDetails.NumberOfDaysRemaining),
                    RemainingLeave = Convert.ToDecimal(leaveDetails.RemainingLeaveDays),
                    LeaveId = leaveDetails.LeaveTypeId,
                    StaffId = staffId,
                    StartDate = leaveDetails.StartDate,
                    EndDate = leaveDetails.EndDate,
                    Reason = leaveDetails.LeaveReason,
                    Status = LeaveStatus.Applied,
                    Active = true,
                    Deleted = false,
                    DeteCreated = DateTime.Now, // Fixed the typo
                };
                _context.LeaveApplications.Add(leaveModel);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<List<LeaveSetup>> GetAllStaffLeaveTypes(string username)
        {
            try
            {
                var currentUser = _userManager.Users.Where(x => x.UserName == username).FirstOrDefault().Id;
                if (currentUser != null)
                {
                    //return await _context.LeaveSetups.Where(x => !x.Deleted && x.Shift.Locations.UserIds == currentUser).ToListAsync();
                    var leaveTypes = await _context.LeaveSetups
                        .Where(x => !x.Deleted && x.Active).Include(d => d.Shift).Include(d => d.Shift.Locations)
                        .ToListAsync();
                    return leaveTypes;
                }
               return null;
            }
            catch (Exception ex)
            {
                throw;
            }
         }

        public async Task<LeaveSetup> GetLeaveTypeById(int id)
        {
            var Leave = new LeaveSetup();
            if (id > 0)
            {
                var leaveType = _context.LeaveSetups.Where(x => x.Id == id && x.Name != null).Include(x => x.Shift).FirstOrDefault();
                if (leaveType != null)
                {
                    return leaveType;
                }
                return Leave;
            }
            return Leave;
        }

        public bool EditLeaveType(LeaveTypeViewModel leaveTypeViewModel)
        {
            if (leaveTypeViewModel != null)
            {
                var leaveTypeEdited = _context.LeaveSetups.Where(x => x.Id == leaveTypeViewModel.Id && !x.Deleted).Include(x => x.Shift).FirstOrDefault();
                if (leaveTypeEdited != null)
                {
                    //leaveTypeEdited.NumberOfDays = leaveTypeViewModel.NumberOfDays;
                    leaveTypeEdited.Name = leaveTypeViewModel.Name;
                   // leaveTypeEdited.CompanyBranchId = leaveTypeViewModel.CompanyBranchId;
                    _context.LeaveSetups.Update(leaveTypeEdited);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteLeaveType(int id)
        {
            if (id != 0)
            {
                var leaveTypeToBeDeleted = _context.LeaveSetups
                    .FirstOrDefault(x => x.Id == id && x.Active && !x.Deleted);

                if (leaveTypeToBeDeleted != null)
                {
                    leaveTypeToBeDeleted.Active = false;
                    leaveTypeToBeDeleted.Deleted = true;
                    _context.LeaveSetups.Update(leaveTypeToBeDeleted);
                    _context.SaveChanges();
                    return true; // Successfully deleted
                }
            }
            return false; // Failed to delete
        }

        public class LeaveSummary
        {
            public int? TotalLeaveDaysUsed { get; set; }
            public int? RemainingLeaveDays { get; set; }
            public int? TotalAnnualLeaveDays { get; set; }
        }

        public LeaveApplication GetEmployeeLeaveById(int id)
        {
            var employeeLeave = new LeaveApplication();
            if (id > 0)
            {
                var staffLeave = _context.LeaveApplications
                .Where(x => x.Id == id && x.Name != null)
                .Include(x => x.Leave)
                .Select(x => new LeaveApplication()
                {
                    Id = x.Id,
                    Name = x.Leave.Name,
                    NumberOfDays = x.NumberOfDays,
                    NumberOfDaysRemaining = x.NumberOfDaysRemaining,
                    RemainingLeave = x.RemainingLeave,
                    RemainingLeaveDays = x.RemainingLeaveDays,
                    Reason = x.Reason,
                    Status = x.Status,
                }).FirstOrDefault();
                return staffLeave;
            }
            return employeeLeave;
        }
        public bool EditEmployeeLeave(RequestLeaveViewModel employeeLeaveViewModel)
        {
            if (employeeLeaveViewModel != null)
            {
                var staffLeave = _context.LeaveApplications.Where(x => x.Id == employeeLeaveViewModel.Id && !x.Deleted).FirstOrDefault();
                if (staffLeave != null)
                {
                    staffLeave.StartDate = employeeLeaveViewModel.StartDate;
                    staffLeave.EndDate = employeeLeaveViewModel.EndDate;
                    staffLeave.NumberOfDays = employeeLeaveViewModel.NumberOfDays;
                    staffLeave.LeaveId = employeeLeaveViewModel.LeaveId;
                    staffLeave.RemainingLeaveDays = employeeLeaveViewModel.RemainingLeaveDays;
                    staffLeave.RemainingLeave = employeeLeaveViewModel.RemainingLeave;
                    staffLeave.Reason = employeeLeaveViewModel.LeaveReason;
                    staffLeave.Status = LeaveStatus.Applied;
                    _context.LeaveApplications.Update(staffLeave);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
