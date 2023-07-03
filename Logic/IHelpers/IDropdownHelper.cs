using Core.Models;
using Core.ViewModels;
using static Core.Enums.Resource_Planing;

namespace Logic.IHelpers
{
    public interface IDropdownHelper
	{
		List<EnumDropDownViewModel> GetGenderDropDown();
		List<Location> GetLocations();
		List<Department> GetDepartments();

	}
}
