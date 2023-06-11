using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

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

		public List<Location> GetLocations(string userName)
		{
			var locations = new List<Location>();
			var currentUser = FindByUserName(userName);
			if (currentUser != null)
			{
				var location = _context.locations.Where(a => a.Id > 0 && a.UserId == currentUser.Id && a.Active && !a.Deleted).Include(f => f.User).ToList();
				if (location.Any())
				{
					locations = location;
				}
			}
			return locations;
		}

		public bool AddLoction(LocationViewModel locationDetails, string userName)
		{
			if (locationDetails != null && userName != null)
			{
				var loggedInuser = FindByUserName(userName);
				if (loggedInuser != null)
				{
					var locationModel = new Location()
					{
						Name = locationDetails.Name,
						AbbreviatedName = locationDetails.AbbreviatedName,
						UserId = loggedInuser.Id,
						Active = true,
						Deleted = false,
						DeteCreated = DateTime.Now,
					};
					_context.locations.Add(locationModel);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public Location GetLocationById(int id, string userName)
		{
			var locations = new Location();
			if (id > 0)
			{
				var user = FindByUserName(userName);
				var locationToBeEdited = _context.locations.Where(x => x.Id == id && x.UserId == user.Id && !x.Deleted).Include(x => x.User).FirstOrDefault();
				if (locationToBeEdited != null)
				{
					locations = locationToBeEdited;
				}
			}
			return locations;
		}

		public bool LocationEdited(LocationViewModel locationViewModel, string userName)
		{
			if (locationViewModel != null)
			{
				var user = FindByUserName(userName);
				var location = _context.locations.Where(x => x.Id == locationViewModel.Id && x.UserId == user.Id && !x.Deleted).FirstOrDefault();
				if (location != null)
				{
					location.Name = locationViewModel.Name;
					location.AbbreviatedName = locationViewModel.AbbreviatedName;
					location.UserId = user.Id;
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
		public List<Department> GetDEepartments(string userName)
		{
			var departments = new List<Department>();
			var currentUser = FindByUserName(userName);
			if (currentUser != null)
			{
				var department = _context.Departments.Where(a => a.Id > 0 && a.UserId == currentUser.Id && a.Active && !a.Deleted).Include(f => f.User).ToList();
				if (department.Any())
				{
					departments = department;
				}
			}
			return departments;
		}
		public bool AddDepartment(DepartmentViewModel departmentDetails, string userName)
		{
			if (departmentDetails != null && userName != null)
			{
				var loggedInuser = FindByUserName(userName);
				if (loggedInuser != null)
				{
					var departmentModel = new Department()
					{
						Name = departmentDetails.Name,
						UserId = loggedInuser.Id,
						Active = true,
						Deleted = false,
						DeteCreated = DateTime.Now,
					};
					_context.Departments.Add(departmentModel);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public Department GetDepartmentById(int id, string userName)
		{
			var departments = new Department();
			if (id > 0)
			{
				var user = FindByUserName(userName);
				var departmentToBeEdited = _context.Departments.Where(x => x.Id == id && x.UserId == user.Id && !x.Deleted).Include(x => x.User).FirstOrDefault();
				if (departmentToBeEdited != null)
				{
					departments = departmentToBeEdited;
				}
			}
			return departments;
		}

		public bool DepartmentEdited(DepartmentViewModel departmentViewModel, string userName)
		{
			if (departmentViewModel != null)
			{
				var user = FindByUserName(userName);
				var department = _context.Departments.Where(x => x.Id == departmentViewModel.Id && x.UserId == user.Id && !x.Deleted).FirstOrDefault();
				if (department != null)
				{
					department.Name = departmentViewModel.Name;
					department.UserId = user.Id;
					department.Active = true;
					department.Deleted = false;
					department.DeteCreated = departmentViewModel.DeteCreated;

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
		public List<Time> GetTimes(string userName)
		{
			var times = new List<Time>();
			var currentUser = FindByUserName(userName);
			if (currentUser != null)
			{
				var time = _context.Times.Where(a => a.Id > 0 && a.UserId == currentUser.Id && a.Active && !a.Deleted).Include(f => f.User).ToList();
				if (time.Any())
				{
					times = time;
				}
			}
			return times;
		}

		public bool AddTime(TimeViewModel timeDetails, string userName)
		{
			if (timeDetails != null && userName != null)
			{
				var loggedInuser = FindByUserName(userName);
				if (loggedInuser != null)
				{
					var timeModel = new Time()
					{
						Name = timeDetails.Name,
						ShiftTime = timeDetails.ShiftTime.ToString(),
						UserId = loggedInuser.Id,
						Active = true,
						Deleted = false,
						DeteCreated = DateTime.Now,
					};
					_context.Times.Add(timeModel);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool AddTimes(TimeOnly timeDetails, string userName)
		{
			if (timeDetails != TimeOnly.MinValue && userName != null)
			{
				var loggedInuser = FindByUserName(userName);
				if (loggedInuser != null)
				{
					var timeModel = new Time()
					{
						Name = null,
						ShiftTime = timeDetails.ToString(),
						UserId = loggedInuser.Id,
						Active = true,
						Deleted = false,
						DeteCreated = DateTime.Now,
					};
					_context.Times.Add(timeModel);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public Time GetTimeById(int id, string userName)
		{
			var times = new Time();
			if (id > 0)
			{
				var user = FindByUserName(userName);
				var timesToBeEdited = _context.Times.Where(x => x.Id == id && x.UserId == user.Id && !x.Deleted).Include(x => x.User).Select(s=> new Time() { 
					Id = s.Id,
					ShiftTime = s.ShiftTime,
					DeteCreated = s.DeteCreated,
				}).FirstOrDefault();
				if (timesToBeEdited != null)
				{
					times = timesToBeEdited;
				}
			}
			return times;
		}

		public bool TimeEdited(TimeViewModel timeViewModel, string userName)
		{
			if (timeViewModel != null)
			{
				var user = FindByUserName(userName);
				var time = _context.Times.Where(x => x.Id == timeViewModel.Id && x.UserId == user.Id && !x.Deleted).FirstOrDefault();
				if (time != null)
				{
					time.Name = timeViewModel.Name;
					time.ShiftTime = timeViewModel.ShiftTime.ToString();
					time.UserId = user.Id;
					time.Active = true;
					time.Deleted = false;
					time.DeteCreated = timeViewModel.DeteCreated;

					_context.Times.Update(time);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool DeleteTime(int id)
		{
			if (id != 0)
			{
				var time = _context.Times.Where(x => x.Id == id && x.Active && !x.Deleted).FirstOrDefault();
				if (time != null)
				{
					time.Active = false;
					time.Deleted = true;
					_context.Times.Update(time);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}
        public bool AddShift(DepartmentViewModel shiftDetails)
        {
            if (shiftDetails != null)
            {
                var shiftData = new Shift()
                {
                    Active = true,
                    DeteCreated = DateTime.Now,
                    Deleted = false,
                    Name = shiftDetails.Name,
                    AbbreviatedName = shiftDetails.AbbreviatedName,
                };
                _context.shifts?.Add(shiftData);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
