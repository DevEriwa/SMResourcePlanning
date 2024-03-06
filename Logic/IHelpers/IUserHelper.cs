using Core.Models;
using Core.ViewModels;
using System.Drawing;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
		Task<ApplicationUser> FindByEmailAsync(string email);
		ApplicationUser FindByUserName(string username);
		Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber);
		Task<ApplicationUser> FindByIdAsync(string Id);
		List<Location> GetLocations();
		bool AddLoction(LocationViewModel locationDetails);
		Location GetLocationById(int id);
		bool LocationEdited(LocationViewModel locationViewModel);
		bool DeleteLocation(int id);
		bool AddDepartment(DepartmentViewModel departmentDetails);
		Department GetDepartmentById(int id);
		List<Department> GetListOfAllDepartment();
		bool DepartmentEdited(DepartmentViewModel departmentViewModel);
		bool DeleteDepartment(int id);
        Task<ApplicationUser> FindByUserNameAsync(string userName);
        Shifts AddShift(ShiftViwModel shiftDetails);
		List<Shifts> GetShifts();
		bool EditShift(ShiftViwModel shiftDetails);
		bool DeleteShift(int id);
		bool AddShiftLocation(ShiftLocationViewModel shiftDetails);
		Task<ShiftsLocation> GetShiftById(int shiftId);
		List<ShiftsLocation> GetShiftLocationList();
		bool EditShiftLocation(ShiftLocationViewModel shiftDetails);
		bool DeleteShiftLocation(ShiftLocationViewModel shiftDetails);
		ShiftsLocation GetShiftLocation(int shiftId);
        List<LeaveApplication> GetStaffLeave(string username);
        LeaveSetup GetAnnualLeave(string username);
        ApplicationUser FindAdminByUserName(string username);
        List<LeaveApplication> GetAllLeave(string currentAdminUsername);
        ApplicationUser FindById(string Id);
        LeaveApplication GetLeaveById(int id);
        bool CompareImages(Image capturedImage, Image newCapturedImage, float similarityThreshold);
        //List<Shifts> GetUserShiftsInLocation(string logedInUser, int locationId);
        List<Shifts> GetUserShiftsInLocations(string logedInUser, List<int> locationIds);
        List<int> GetUserLocationId(string userId);
        StaffClockIn GetUserPunchActionForCurrentDay(string userId);
        Response PunchInService(PunchingViewModel model);
        Response PunchOutService(PunchingViewModel model);
        List<GetAllShiftForAdmin> GetListOfUserOnShift(string loggedInUsername);

		List<UserViewModel> GetUser(string userName);
		List<LeaveSetup> GetLeave();
		AdminDashboardViewModel GetAdminDashboardData(string userName);
		ApplicationUser GetOneUser();
		AllShiftViewModel GetShiftList();

	}
}
