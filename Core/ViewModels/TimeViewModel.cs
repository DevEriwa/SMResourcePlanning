﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class TimeViewModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public bool Active { get; set; }
		public bool Deleted { get; set; }
		public DateTime DeteCreated { get; set; }
		public string? ShiftTime { get; set; }
		public TimeOnly ShiftTimenOnly { get; set; }
		public string? UserId { get; set; }
	}
}
