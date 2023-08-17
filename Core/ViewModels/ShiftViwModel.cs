using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class ShiftViwModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public bool Active { get; set; }
		public bool Deleted { get; set; }
		public DateTime DeteCreated { get; set; }
		public string? AbbreviatedName { get; set; }
		public bool? IsFixed { get; set; }
		public string? Activity { get; set; }
		public string? StartTime { get; set; }
		public string? EndTime { get; set; }
		public string? UnpaidTime { get; set; }
		public double? FixedAmount { get; set; }
		public double? HourlyPay { get; set; }
		public int? LocationId { get; set; }
	}
}
