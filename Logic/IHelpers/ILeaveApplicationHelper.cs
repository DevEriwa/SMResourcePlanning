using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface ILeaveApplicationHelper
    {
        bool CreateLeave(LeaveViewModel leaveDetails);
        Task<List<LeaveSetup>> GetAllStaffLeaveTypes(string username);
        Task<LeaveSetup> GetLeaveTypeById(int id);
        bool StaffRequestLeave(RequestLeaveViewModel leaveDetails, string staffId);
        string EditLeaveType(LeaveTypeViewModel leaveTypeViewModel);
        string DeleteLeaveType(int id);
    }
}
