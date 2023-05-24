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
	}
}
