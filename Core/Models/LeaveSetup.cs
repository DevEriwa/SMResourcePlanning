using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class LeaveSetup:BaseModel
    {
       
        public string? Abbreviations { get; set; }
        public bool DeductFromAnnualLeave { get; set; }
        public bool HoursDeductedFromTimesheet { get; set; }
        // Add other fields as needed

    }
}
