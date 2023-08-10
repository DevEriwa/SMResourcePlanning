using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;

namespace Core.Models
{
	public class Attendance : BaseModel
	{
		[Display(Name = "Check In Time")]
		public DateTime CheckInTime { get; set; }

		[Display(Name = "check Out Time")]
		public DateTime CheckOutTime { get; set; }
		public int ShiftId { get; set; }
		[Display(Name = "Shift")]
		[ForeignKey("ShiftId")]
		public virtual Shifts Shift { get; set; }
		public string StaffId { get; set; }
		[Display(Name = "Staff")]
		[ForeignKey("StaffId")]
		public virtual ApplicationUser Staff { get; set; }

		[Display(Name = "Break")]
		public int Break { get; set; }

		[Display(Name = "TimeSpent")]
		public int TimeSpent { get; set; }

		[Display(Name = "OverTime")]
		public int OverTime { get; set; }
		public string? Date { get; set; }
	}
}
