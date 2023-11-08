using Core.Models;
using Core.ViewModels;
using static Core.Enums.Resource_Planing;
using static Logic.Helpers.DropdownHelper;

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
		List<DropdownEnumModel> GetLeaveStatus();
        Task<List<Shifts>> GetStaffShiftDropDown(string userName);
        //List<Shifts> GetStaffShifts();
        Task<List<Shifts>> GetStaffShifts();
    }
}
