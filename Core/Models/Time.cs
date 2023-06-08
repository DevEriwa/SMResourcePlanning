using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Time :BaseModel
	{
		public string? ShiftTime { get; set; }
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
		[NotMapped]
		public TimeOnly ShiftTimenOnly { get; set; }
	}
}
