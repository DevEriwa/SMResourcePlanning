using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class SetupRota : BaseModel
	{
		public int? ActivityId { get; set; }
		[ForeignKey("ActivityId")]
		public virtual Activity? Activity { get; set; }
		public int? ShiftId { get; set; }
		[ForeignKey("ShiftId")]
		public virtual Shift? Shifts { get; set; }
		public int? TimeId { get; set; }
		[ForeignKey("TimeId")]
		public virtual Time? Time { get; set; }
		public int? LocationId { get; set; }
		[ForeignKey("LocationId")]
		public virtual Location? Locations { get; set; }
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
	}
}
