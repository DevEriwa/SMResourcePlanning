using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
	public class Leave : BaseModel
	{
        public string? Name { get; set; }

        [Display(Name = "Number Of Days")]
		public int? NumberOfDays { get; set; }

		[Display(Name = "Deduct from annual leave")]
		public bool DeductFromAnnualLeave { get; set; }
		
		[Display(Name = "Hours Deducted from timesheet")]
		public bool HoursDeductedFromTimesheet { get; set; }

        public string? Abbreviations { get; set; }
    }
}
