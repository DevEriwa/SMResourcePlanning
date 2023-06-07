﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Department : BaseModel
    {
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
	}
}
