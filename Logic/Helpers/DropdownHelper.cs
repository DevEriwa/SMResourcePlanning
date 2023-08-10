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
			var common = new ApplicationUser()
			{
				Id = "",
				FirstName = "-- Select --"
			};
			var usersInRota = _context.ApplicationUser.Where(a => a.DisplayOnRota).Select(c => new ApplicationUser()
			{
				Id = c.Id,
				FirstName = c.Name,
			}).ToList();
			usersInRota.Insert(0, common);
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
    }
}
