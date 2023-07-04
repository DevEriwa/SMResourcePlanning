using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class RotaObject
	{
		public string? Date { get; set; }
		public int? ShiftId { get; set; }
		public virtual Shifts shift { get; set; }
		public string? TRange { get; set; }
	}
}
