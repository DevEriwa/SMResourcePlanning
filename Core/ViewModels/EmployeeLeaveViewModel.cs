using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Resource_Planing;
using System.Xml.Linq;

namespace Core.ViewModels
{
    public class EmployeeLeaveViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveReason { get; set; }
        public decimal NumberOfDays { get; set; }
        public string LeaveType { get; set; }
        public string Name { get; set; }
        public int LeaveId { get; set; }
        public int AnnaulLeaveId { get; set; }
        public int OtherTypeLeaveId { get; set; }
        public string StartDateInString { get; set; }
        [Display(Name = "Remaning Leave")]
        public decimal RemainingLeave { get; set; }
        public string RemainingLeaveDays { get; set; }
        public string EndDateInString { get; set; }
        public LeaveStatus LeaveStatus { get; set; }
    }
}
