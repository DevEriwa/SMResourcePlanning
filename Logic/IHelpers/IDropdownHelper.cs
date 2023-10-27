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
		List<ApplicationUser> GetAllUsersInRota();
		Task<List<Country>> GetCountry();
		Task<List<State>> GetState();
		List<Shifts> GetShifts();
        List<LeaveSetup> AllLeaveType(string name);
    }
}
