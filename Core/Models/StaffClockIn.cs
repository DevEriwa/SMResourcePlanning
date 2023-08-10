using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
	public class StaffClockIn
	{
		public int Id { get; set; }
		public Guid ClockId { get; set; }
		public double ClockInLatitude { get; set; }
		public double ClockInLongitude { get; set; }
		public double ClockOutLatitude { get; set; }
		public double ClockOutLongitude { get; set; }
		public DateTime DateTimeIn { get; set; }
		public DateTime DateTimeOut { get; set; }
		public string StaffId { get; set; }
		[Display(Name = "Staff")]
		[ForeignKey("StaffId")]
		public virtual ApplicationUser Staff { get; set; }
		public DateTime ClockInDate { get; set; }
		[NotMapped]
		public string ForClockInReport { get; set; }

		[NotMapped]
		public double TimeSpentMonthly { get; set; }
		[NotMapped]
		public double TimeSpent => (DateTimeOut - DateTimeIn).TotalHours;
	}
}
