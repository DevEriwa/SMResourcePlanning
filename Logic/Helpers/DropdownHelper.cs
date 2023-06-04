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

		public async Task<List<CommonDropdowns>> GetDropdownsByKey(DropdownEnums dropdownEnums)
        {
            try
            {
                var common = new CommonDropdowns()
                {
                    Id = 0,
                    Name = "Select",
                };
                var listOfDropdownEnums = await _context.CommonDropdowns.Where(a => !a.Deleted && a.DropdownKey == (int)dropdownEnums).OrderBy(a => a.Name).ToListAsync();
                listOfDropdownEnums.Insert(0, common);
                return listOfDropdownEnums;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Department> GetDepartments(string userName)
        {
            var departments = new List<Department>();
            var currentUser = _userHelper.FindByUserName(userName);
            if (currentUser != null)
            {

                var common = new Department()
                {
                    Id = 0,
                    Name = "-- Select --"
                };
                var department = _context.Departments.Where(a => a.Id > 0 && a.UserId == currentUser.Id && a.Active && !a.Deleted).Include(f => f.User).OrderBy(s=>s.Name)
					.Select(c => new Department()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();
                department.Insert(0, common);
                if (department.Any())
                {
                    departments = department;
                }
            }
            return departments;
        }

        public List<Location> GetLocations(string userName)
        {
            var locations = new List<Location>();
            var currentUser = _userHelper.FindByUserName(userName);
            if (currentUser != null)
            {

                var common = new Location()
                {
                    Id = 0,
                    Name = "-- Select --"
                };
                var location = _context.locations.Where(a => a.Id > 0 && a.UserId == currentUser.Id && a.Active && !a.Deleted).Include(f => f.User).OrderBy(d =>d.AbbreviatedName)
                    .Select(c => new Location()
                    {
                        Id = c.Id,
                        AbbreviatedName = c.AbbreviatedName,
                    }).ToList();
                location.Insert(0, common);
                if (location.Any())
                {
                    locations = location;
                }
            }
            return locations;
        }
    }
}
