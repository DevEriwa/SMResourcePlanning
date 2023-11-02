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

        public LeaveApplicationHelper
        (AppDbContext context, 
        IUserHelper userHelper,
        UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userHelper = userHelper;
            _userManager = userManager;
        }

        public bool CreateLeave(LeaveViewModel leaveDetails)
        {
            if (leaveDetails != null)
            {
                var leaveModel = new LeaveSetup()
                {
                    Name = leaveDetails.Name,
                    Abbreviations = leaveDetails.Abbreviations,
                    DeductFromAnnualLeave = leaveDetails.DeductFromAnnualLeave,
                    HoursDeductedFromTimesheet = leaveDetails.HoursDeductedFromTimesheet,
                    //shiftId = leaveDetails.ShiftId,
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

        public bool StaffRequestLeave(RequestLeaveViewModel leaveDetails, string staffId)
        {
            if (string.IsNullOrEmpty(staffId) || leaveDetails == null)
            {
                return false;
            }
            var existingLeaveApplication = _context.LeaveApplications
                .FirstOrDefault(x => x.StaffId == staffId);
            if (existingLeaveApplication != null)
            {
                return false;
            }
            var leaveModel = new LeaveApplication()
            {
               // shiftId = leaveDetails.ShiftfId,
                LeaveId = leaveDetails.LeaveTypeId,
                StaffId = staffId,
                StartDate = leaveDetails.StartDate,
                EndDate = leaveDetails.EndDate,
                Status = LeaveStatus.Applied,
                Active = true,
                Deleted = false,
                DeteCreated = DateTime.Now, // Fixed the typo
            };
            _context.LeaveApplications.Add(leaveModel);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<LeaveSetup>> GetAllStaffLeaveTypes(string username)
        {
            var leaveType = new List<LeaveSetup>();
            try
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);

                if (currentUser != null)
                {
                    var leaveTypes = await _context.LeaveSetups
                        .Where(x => !x.Deleted && x.Active)
                        .ToListAsync();
                    return leaveTypes;
                }
                return leaveType;
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

        public string EditLeaveType(LeaveTypeViewModel leaveTypeViewModel)
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
                    return "LeaveType Successfully Updated";
                }
            }
            return "LeaveType failed To Update";
        }

        public string DeleteLeaveType(int id)
        {
            if (id != 0)
            {
                var leaveTypeToBeDeleted = _context.LeaveSetups.Where(x => x.Id == id && x.Active && !x.Deleted).FirstOrDefault();
                if (leaveTypeToBeDeleted != null)
                {
                    leaveTypeToBeDeleted.Active = false;
                    leaveTypeToBeDeleted.Deleted = true;
                    _context.LeaveSetups.Update(leaveTypeToBeDeleted);
                    _context.SaveChanges();
                    return "LeaveType Successfully Deleted";
                }
            }
            return "LeaveType failed To Delete";
        }
        public class LeaveSummary
        {
            public int? TotalLeaveDaysUsed { get; set; }
            public int? RemainingLeaveDays { get; set; }
            public int? TotalAnnualLeaveDays { get; set; }
        }


    }
}
