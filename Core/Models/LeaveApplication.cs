using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Core.Enums.Resource_Planing;

namespace Core.Models
{
    public class LeaveApplication:BaseModel
    {
        public int? LeaveId { get; set; }
        [Display(Name = "Leave")]
        [ForeignKey("LeaveId")]
        public virtual LeaveSetup Leave { get; set; }
        public string StaffId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("StaffId")]
        public virtual ApplicationUser User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public LeaveStatus? Status { get; set; }
        public DateTime DateApproved { get; set; }
    }
}
