using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
	public class Location : BaseModel
	{
		public string? AbbreviatedName { get; set; }
		public string? UserIds { get; set; }
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
		[Display(Name = "Latitude")]
		public double Latitude { get; set; }
		[Display(Name = "CloclIn Radius")]
		public double AcceptedRadius { get; set; }
	}
}
