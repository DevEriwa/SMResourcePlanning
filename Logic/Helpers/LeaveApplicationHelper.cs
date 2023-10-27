using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.EntityFrameworkCore;
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

        public LeaveApplicationHelper(AppDbContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
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
    }
}
