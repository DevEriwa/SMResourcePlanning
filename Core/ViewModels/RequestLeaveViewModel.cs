using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Resource_Planing;

namespace Core.ViewModels
{
    public class RequestLeaveViewModel
    {
        public int Id { get; set; }
        public string? StaffId { get; set; }
        public int ShiftfId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? LeaveReason { get; set; }
        public string? leaveTypeName { get; set; }
        public decimal NumberOfDays { get; set; }
        public string? LeaveTypeName { get; set; }
        public string? Name { get; set; }
        public int LeaveTypeId { get; set; }
        public int AnnaulLeaveId { get; set; }
        public int OtherTypeLeaveId { get; set; }
        public string? StartDateInString { get; set; }
        [Display(Name = "Remaning Leave")]
        public decimal RemainingLeave { get; set; }
        public string? RemainingLeaveDays { get; set; }
        public string? NumberOfDaysRemaining { get; set; }
        public string? EndDateInString { get; set; }
        public LeaveStatus LeaveStatus { get; set; }
        public int LeaveId { get; set; }
    }
}
