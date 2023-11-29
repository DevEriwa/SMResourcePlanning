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
        public int? LocationId { get; set; }
        public string? Location { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? TRange { get; set; }
        public string? UnpaidTime { get; set; }
        public double? FixedAmount { get; set; }
        public double? HourlyPay { get; set; }
		public string? UserId { get; set; }
		public string? Year { get; set; }
	}
}
