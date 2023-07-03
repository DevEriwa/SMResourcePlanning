using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class StaffRota
	{
		[Key]
		public int Id { get; set; }
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser User { get; set; }
		public string? RotaObjectString { get; set; }
		public string? Year { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? DateCreated { get; set; }
		[NotMapped]
		public RotaObject[]? RotaObject { get; set; }
		[NotMapped]
		public RotaObject[]? RotaObjectGet
		{
			get
			{
				return JsonConvert.DeserializeObject<RotaObject[]>(RotaObjectString);
			}
		}
	}
}
