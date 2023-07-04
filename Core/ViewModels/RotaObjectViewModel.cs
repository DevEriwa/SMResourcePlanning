using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class RotaObjectViewModel
	{
		public string? Date { get; set; }
		public DateTime Datee { get; set; }
		public int? ShiftId { get; set; }
		public string? TRange { get; set; }
		public int WeekCount { get; set; }
		public string? UserId { get; set; }
		public string? Year { get; set; }
	}
}
