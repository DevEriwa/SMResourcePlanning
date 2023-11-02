using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class LeaveViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Abbreviations { get; set; }
        public bool DeductFromAnnualLeave { get; set; }
        public bool HoursDeductedFromTimesheet { get; set; }
        public int UserId { get; set; } // Link to the user who applied for leave
        public string? LeaveType { get; set; }
        public int ShiftId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeteCreated { get; set; }
    }
}
