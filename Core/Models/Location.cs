using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Location : BaseModel
	{
		public string? AbbreviatedName { get; set; }
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
	}
}
