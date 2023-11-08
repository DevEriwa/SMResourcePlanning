using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class LeaveSetup:BaseModel
    {
        public int? ShiftId { get; set; }
        [Display(Name = "Shift")]
        [ForeignKey("ShiftId")]
        public virtual Shifts Shift { get; set; }
        public string? Abbreviations { get; set; }
        public bool DeductFromAnnualLeave { get; set; }
        public bool HoursDeductedFromTimesheet { get; set; }
        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }
    }
}
