using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Resource_Planing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;
using System.Collections;

namespace Core.Models
{
    public class EmployeeLeave: BaseModel
    {

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        public string? LeaveType { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Leave Reason")]
        public string? LeaveReason { get; set; }
        public LeaveStatus? LeaveStatus { get; set; }
        [Display(Name = "Staff Name")]
        public string? StaffName { get; set; }
        [Display(Name = "User")]
        [ForeignKey("StaffName")]
        public virtual ApplicationUser? User { get; set; }


    }
}
