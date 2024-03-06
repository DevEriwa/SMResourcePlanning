using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class AdminDashboardViewModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int UserCount { get; set; }
		public int LeaveCount { get; set; }
		public int DepartmentCount { get; set; }
		public int ShiftCount { get; set; }
		public List<UserViewModel> Users { get; set; }
		public List<LeaveSetup> Leave { get; set; }
		public List<Department> Departments { get; set; }
		public List<Shifts> Shifts { get; set; }
	}
}
