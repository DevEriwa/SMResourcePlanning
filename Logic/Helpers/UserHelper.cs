using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Logic.Helpers
{
    public class UserHelper : IUserHelper
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public UserHelper(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
			_context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            return await _userManager.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users.Where(s => s.PhoneNumber == phoneNumber)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.Where(s => s.Email == email)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByIdAsync(string Id)
        {
            return await _userManager.Users.Where(s => s.Id == Id)?.FirstOrDefaultAsync();
        }

		public ApplicationUser FindByUserName(string username)
		{
			return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefault();
		}

		public List<Location> GetLocations()
		{
			var locations = new List<Location>();
			var location = _context.locations.Where(a => a.Id > 0 && a.Active && !a.Deleted).ToList();
			if (location.Any())
			{
				locations = location;
			}
			return locations;
		}

		public bool AddLoction(LocationViewModel locationDetails)
		{
			if (locationDetails != null)
			{
				var locationModel = new Location()
				{
					Name = locationDetails.Name,
					AbbreviatedName = locationDetails.AbbreviatedName,
					Active = true,
					Deleted = false,
					DeteCreated = DateTime.Now,
				};
				_context.locations.Add(locationModel);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

		public Location GetLocationById(int id)
		{
			var locations = new Location();
			if (id > 0)
			{
				var locationToBeEdited = _context.locations.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
				if (locationToBeEdited != null)
				{
					locations = locationToBeEdited;
				}
			}
			return locations;
		}

		public bool LocationEdited(LocationViewModel locationViewModel)
		{
			if (locationViewModel != null)
			{
				var location = _context.locations.Where(x => x.Id == locationViewModel.Id && !x.Deleted).FirstOrDefault();
				if (location != null)
				{
					location.Name = locationViewModel.Name;
					location.AbbreviatedName = locationViewModel.AbbreviatedName;
					location.Active = true;
					location.Deleted = false;
					location.DeteCreated = locationViewModel.DeteCreated;
					
					_context.locations.Update(location);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool DeleteLocation(int id)
		{
			if (id != 0)
			{
				var location = _context.locations.Where(x => x.Id == id && x.Active && !x.Deleted).FirstOrDefault();
				if (location != null)
				{
					location.Active = false;
					location.Deleted = true;
					_context.locations.Update(location);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}
		
		public bool AddDepartment(DepartmentViewModel departmentDetails)
		{
			if (departmentDetails != null)
			{
				var departmentModel = new Department()
				{
					Name = departmentDetails.Name,
					Active = true,
					Deleted = false,
					DeteCreated = DateTime.Now,
				};
				_context.Departments.Add(departmentModel);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

		public List<Department> GetListOfAllDepartment()
		{
			var departments = new List<Department>();
			var departmentToBeEdited = _context.Departments.Where(x => x.Active && !x.Deleted).ToList();
			if (departmentToBeEdited != null)
			{
				departments = departmentToBeEdited;
			}
			return departments;
		}
		public Department GetDepartmentById(int id)
		{
			var departments = new Department();
			if (id > 0)
			{
				var departmentToBeEdited = _context.Departments.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
				if (departmentToBeEdited != null)
				{
					departments = departmentToBeEdited;
				}
			}
			return departments;
		}

		public bool DepartmentEdited(DepartmentViewModel departmentViewModel)
		{
			if (departmentViewModel != null)
			{
				var department = _context.Departments.Where(x => x.Id == departmentViewModel.Id && !x.Deleted).FirstOrDefault();
				if (department != null)
				{
					department.Name = departmentViewModel.Name;
					department.Active = true;
					department.Deleted = false;

					_context.Departments.Update(department);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool DeleteDepartment(int id)
		{
			if (id != 0)
			{
				var department = _context.Departments.Where(x => x.Id == id && x.Active && !x.Deleted).FirstOrDefault();
				if (department != null)
				{
					department.Active = false;
					department.Deleted = true;
					_context.Departments.Update(department);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

        public bool AddShift(ShiftViwModel shiftDetails)
        {
            if (shiftDetails != null)
            {
				var shiftData = new Shifts()
				{
					Active = true,
					DeteCreated = DateTime.Now,
					Deleted = false,
					Name = shiftDetails.Name,
					AbbreviatedName = shiftDetails.AbbreviatedName,
					IsFixed = shiftDetails.IsFixed,
					Activity = shiftDetails.Activity,
					StartTime = shiftDetails.StartTime,
					EndTime = shiftDetails.EndTime,
					UnpaidTime = shiftDetails.UnpaidTime,
					FixedAmount = shiftDetails.FixedAmount,
					LocationId = shiftDetails.LocationId,
				};
				_context.shift?.Add(shiftData);
				_context.SaveChanges();
				return true;
			}
            return false;
        }
		public List<Shifts> GetShifts()
		{
			var shifts = new List<Shifts>();
			var shift = _context.shift.Where(a => a.Active && !a.Deleted).ToList();
			if (shift.Any())
			{
				shifts = shift;
			}
			return shifts;
		}

		public bool EditShift(ShiftViwModel shiftDetails)
		{
			if (shiftDetails != null)
			{
				var productVaccineEdit = _context.shift.Where(c => c.Id == shiftDetails.Id).FirstOrDefault();
				if (productVaccineEdit != null)
				{
					productVaccineEdit.Name = shiftDetails.Name;
					productVaccineEdit.AbbreviatedName = shiftDetails.AbbreviatedName;
					productVaccineEdit.IsFixed = shiftDetails.IsFixed;
					productVaccineEdit.Activity = shiftDetails.Activity;
					productVaccineEdit.StartTime = shiftDetails.StartTime;
					productVaccineEdit.EndTime = shiftDetails.EndTime;
					productVaccineEdit.UnpaidTime = shiftDetails.UnpaidTime;
					productVaccineEdit.FixedAmount = shiftDetails.FixedAmount;
					productVaccineEdit.LocationId = shiftDetails.LocationId;
				}
				_context.shift.Update(productVaccineEdit);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

	}
}
