using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;
using static Core.Enums.Resource_Planing;

namespace Logic.Helpers
{
    public class DropdownHelper : IDropdownHelper
	{
		
		private readonly AppDbContext _context;
		private readonly IUserHelper _userHelper;
		private UserManager<ApplicationUser> _userManager;

        public DropdownHelper(AppDbContext context, UserManager<ApplicationUser> userManager, IUserHelper userHelper)
		{
			_context = context;
			_userHelper = userHelper;
			_userManager = userManager;
		}

		public string GetEnumDescription(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			if (attributes != null && attributes.Any())
			{
				var des = attributes.First().Description;
				return des;
			}
			return value.ToString();
		}

		public List<EnumDropDownViewModel> GetTitle()
		{
			var data = new List<EnumDropDownViewModel>();
			var titles = ((Title[])Enum.GetValues(typeof(Title)))
			   .Select(c => new EnumDropDownViewModel() { Id = (int)c, Name = c.ToString() }).ToList();
			foreach (var item in titles)
			{
				var enumId = (Title)item.Id;
				var descriptions = GetEnumDescription(enumId);
				var mydata = new EnumDropDownViewModel()
				{
					Name = descriptions,
					Id = item.Id,
				};
				data.Add(mydata);
			}
			return data;
		}

		public List<EnumDropDownViewModel> GetMaritalStatus()
		{
			var data = new List<EnumDropDownViewModel>();
			var maritalStatus = ((MaritalStatus[])Enum.GetValues(typeof(MaritalStatus)))
			   .Select(c => new EnumDropDownViewModel() { Id = (int)c, Name = c.ToString() }).ToList();
			foreach (var item in maritalStatus)
			{
				var enumId = (MaritalStatus)item.Id;
				var descriptions = GetEnumDescription(enumId);
				var mydata = new EnumDropDownViewModel()
				{
					Name = descriptions,
					Id = item.Id,
				};
				data.Add(mydata);
			}
			return data;
		}

		public List<EnumDropDownViewModel> GetStatus()
		{
			var data = new List<EnumDropDownViewModel>();
			var status = ((Status[])Enum.GetValues(typeof(Status)))
			   .Select(c => new EnumDropDownViewModel() { Id = (int)c, Name = c.ToString() }).ToList();
			foreach (var item in status)
			{
				var enumId = (Status)item.Id;
				var descriptions = GetEnumDescription(enumId);
				var mydata = new EnumDropDownViewModel()
				{
					Name = descriptions,
					Id = item.Id,
				};
				data.Add(mydata);
			}
			return data;
		}

		public List<EnumDropDownViewModel> GetGenderDropDown()
        {
            try
            {
				var data = new List<EnumDropDownViewModel>();
				var gender = ((Gender[])Enum.GetValues(typeof(Gender)))
				   .Select(c => new EnumDropDownViewModel() { Id = (int)c, Name = c.ToString() }).ToList();
				foreach (var item in gender)
				{
					var enumId = (Gender)item.Id;
					var descriptions = GetEnumDescription(enumId);
					var mydata = new EnumDropDownViewModel()
					{
						Name = descriptions,
						Id = item.Id,
					};
					data.Add(mydata);
				}
				return data;
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Department> GetDepartments()
        {
            var departments = new List<Department>();
			var common = new Department()
			{
				Id = 0,
				Name = "-- Select --"
			};
			var department = _context.Departments.Where(a => a.Id > 0 && a.Active && !a.Deleted).Select(c => new Department()
			{
				Id = c.Id,
				Name = c.Name,
			}).ToList();
			department.Insert(0, common);
			if (department.Any())
			{
				departments = department;
			}
			return departments;
        }

		public List<Location> GetLocations()
        {
            var locations = new List<Location>();
			var common = new Location()
			{
				Id = 0,
				Name = "-- Select --"
			};
			var location = _context.locations.Where(a => a.Id > 0 && a.Active && !a.Deleted).Select(c => new Location()
			{
				Id = c.Id,
				Name = c.AbbreviatedName,
			}).ToList();
			location.Insert(0, common);
			if (location.Any())
			{
				locations = location;
			}
			return locations;
        }


		public List<ApplicationUser> GetAllUsersInRota()
		{
			var users = new List<ApplicationUser>();
			var usersInRota = _context.ApplicationUser.Where(a => a.DisplayOnRota).Select(c => new ApplicationUser()
			{
				Id = c.Id,
				FirstName = c.Name,
			}).ToList();
			if (usersInRota.Any())
			{
				users = usersInRota;
			}
			return users;
		}


        public async Task<List<Country>> GetCountry()
        {
            var common = new Country()
            {
                Id = 0,
                Name = "-- Select --"

            };
            var selectedCountry = await _context.Country.OrderBy(x => x.Name).Where(x => x.Active && !x.Deleted).ToListAsync();
            if (selectedCountry != null)
            {
                selectedCountry.Insert(0, common);
                return selectedCountry;
            }
            return null;


        }

        public async Task<List<State>> GetState()
        {
            var common = new State()
            {
                Id = 0,
                Name = "-- Select --"

            };
            var selectedState = await _context.State.OrderBy(x => x.Name).Where(x => x.Active && !x.Deleted).ToListAsync();
            if (selectedState != null)
            {
                selectedState.Insert(0, common);
                return selectedState;
            }
            return null;
        }

        public List<Shifts> GetShifts()
        {
            var shifts = new List<Shifts>();
            var common = new Shifts()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var shift = _context.shift.Where(a => a.Id > 0 && a.Active && !a.Deleted)
			.Select(c => new Shifts()
            {
                Id = c.Id,
                Name = c.AbbreviatedName,
            }).ToList();
            shift.Insert(0, common);
            if (shift.Any())
            {
                shifts = shift;
            }
            return shifts;
        }
        public async Task<List<Shifts>> GetStaffShifts()
        {
            try
            {
                Shifts common = new Shifts(); 
                List<Shifts> shifts = new List<Shifts>();

                var query = _context.shift
                    .Where(a => a.Id > 0 && a.Active && !a.Deleted)
                    .Include(s => s.Locations);

                shifts = await query
                    .Select(c => new Shifts
                    {
                        Id = c.Id,
                        Name = c.Locations.Name,
                    })
                    .ToListAsync();

                shifts.Insert(0, common);

                return shifts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }


        public async Task<List<Shifts>> GetStaffShiftss(string userName)
        {
            var getUser = _context.ApplicationUser.Where(x => x.UserName == userName)?.FirstOrDefault();
            var shifts = new List<Shifts>();
            var common = new Shifts()
            {
                Id = 0,
                Name = "-- Select --"
            };
            if (getUser != null)
            {
                var selectedBranches = await _context.shift
                .OrderBy(x => x.Locations.Name)
                .Where(x => x.Locations.UserIds != null && x.Locations.UserIds.Contains(getUser.Id.ToString()) && !x.Deleted && x.Active)
                .ToListAsync();

                if (selectedBranches != null)
                {
                    selectedBranches.Insert(0, common);
                    return selectedBranches;
                }
                return null;
            }
            return null;
            
        }
        public class DropdownEnumModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<DropdownEnumModel> GetLeaveStatus()
        {
            var common = new DropdownEnumModel()
            {
                Id = 0,
                Name = "-- Select --"

            };
            var enumList = ((LeaveStatus[])Enum.GetValues(typeof(LeaveStatus)))
            .Select(c => new DropdownEnumModel() { Id = (int)c, Name = c.ToString() })
            .Where(x => x.Id != (int)LeaveStatus.Absence)
            .ToList();
            return enumList;
        }

        public List<LeaveSetup> AllLeaveType(string name)
        {
            var leaves = new List<LeaveSetup>();
            var getUser = _userManager.Users.Where(x => x.UserName == name)?.FirstOrDefault();
            var common = new LeaveSetup()
            {
                Id = 0,
                Name = "-- Select --"
            };
            if (getUser != null)
            {
                var selectedLeave = _context.LeaveSetups.OrderBy(x => x.Name).Where(x => x.Active && !x.Deleted).ToList();
                if (selectedLeave != null)
                {
                    selectedLeave.Insert(0, common);
                    return selectedLeave;
                }
            }
            return leaves;
        }

        public async Task<List<Shifts>> GetStaffShiftDropDown(string userName)
        {
            var getUser = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.UserName == userName);
            if (getUser != null)
            {
                var selectedBranches = await _context.shift
                    .Where(x => !x.Deleted && x.Active)
                    .OrderBy(x => x.Name)
                    .ToListAsync();
                if (selectedBranches != null)
                {
                    var common = new Shifts
                    {
                        Id = 0,
                        Name = "-- Select --"
                    };
                    selectedBranches.Insert(0, common);
                    return selectedBranches;
                }
            }
            return null;
        }

    }
}
