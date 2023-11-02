using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Shifts : BaseModel
    {
        public string? AbbreviatedName { get; set; }
        public bool? IsFixed { get; set; }
        public string? Activity { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? UnpaidTime { get; set; }
        public double? FixedAmount { get; set; }
        public double? HourlyPay { get; set; }
		public int? LocationId { get; set; }
		[ForeignKey("LocationId")]
		public virtual Location? Locations { get; set; }
        
        // the maximum staff allowed in a shift
        public int MaxStaff { get; set; }
    }
}
