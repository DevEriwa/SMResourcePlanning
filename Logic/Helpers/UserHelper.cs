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
using static Core.Enums.Resource_Planing;

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

        public Shifts AddShift(ShiftViwModel shiftDetails)
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
					HourlyPay = shiftDetails.HourlyPay,
					LocationId = shiftDetails.LocationId,
				};
				_context.shift?.Add(shiftData);
				_context.SaveChanges();
                var result = _context.shift.Where(s => s.Id == shiftData.Id).Include(l => l.Locations).FirstOrDefault();
                return result;
			}
            return null;
        }
		public List<Shifts> GetShifts()
		{
			var shifts = new List<Shifts>();
			var shift = _context.shift.Where(a => a.Active && !a.Deleted).Include(v=> v.Locations).ToList();
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
					productVaccineEdit.HourlyPay = shiftDetails.HourlyPay;
					productVaccineEdit.LocationId = shiftDetails.LocationId;
				}
				_context.shift.Update(productVaccineEdit);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

		public bool DeleteShift(int id)
		{
			if (id != 0)
			{
				var shift = _context.shift.Where(x => x.Id == id && x.Active && !x.Deleted).FirstOrDefault();
				if (shift != null)
				{
					shift.Active = false;
					shift.Deleted = true;
					_context.shift.Update(shift);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public List<EmployeeLeave> GetListOfAllLeave()
		{
			var leaves = new List<EmployeeLeave>();
			var leavesApplied = _context.EmployeeLeaves.Where(x => x.StaffName == null && leaves.Count() > 0).ToList();
			if (leavesApplied != null)
			{
				leaves = leavesApplied;
			}
			return leaves;
		}

        public List<EmployeeLeave> GetEmployeeLeave(string username)
        {
            var listOfLeaves = new List<EmployeeLeave>();
            var currentUserId = GetCurrentUserId(username);
            if (currentUserId != null)
            {
                var leaves = _context.EmployeeLeaves.Where(x => (x.Active == true) && (x.Deleted == false) && (x.StaffName == currentUserId) && (x.DeteCreated != DateTime.MinValue))?.ToList();
                leaves = leaves.OrderByDescending(x => x.DeteCreated).ToList();
                if (leaves.Any())
                {
                    return leaves;
                }
            }
            return listOfLeaves;
        }

        public string GetCurrentUserId(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefaultAsync().Result.Id?.ToString();
        }

        public Leave GetAnnualLeave(string username)
        {
            var user = _userManager.Users.Where(s => s.UserName == username).FirstOrDefault();
            return _context.Leave.Where(x => x.Active == true && x.Deleted == false && x.Name.ToLower().Contains("annual") && x.Name == user.Name)?.FirstOrDefault();
        }

        public EmployeeLeave GetLeaveById(int id)
        {
            var leave = _context.EmployeeLeaves.Where(x => x.Id == id  && x.StaffName != null && !x.Deleted).Include(s => s.User).FirstOrDefault();
            if (leave != null)
            {
                return (leave);
            }
            return null;
        }

		public EmployeeLeave GetEmployeeLeaveById(int id)
		{
			var employeeLeave = new EmployeeLeave();
			if (id > 0)
			{
				var staffLeave = _context.EmployeeLeaves.Where(x => x.Id == id && x.Name != null).Select(x => new EmployeeLeave()
				{
					Id = x.Id,
					Name = x.Name,
					StartDate = x.StartDate,
					EndDate = x.EndDate,
					LeaveReason = x.LeaveReason,
					LeaveStatus = x.LeaveStatus,
					LeaveType = x.LeaveType,
				}).FirstOrDefault();
				return staffLeave;
			}
			return employeeLeave;
		}

		public string EditEmployeeLeave(EmployeeLeaveViewModel employeeLeaveViewModel)
        {
            if (employeeLeaveViewModel != null)
            {
                var staffLeave = _context.EmployeeLeaves.Where(x => x.Id == employeeLeaveViewModel.Id && !x.Deleted).FirstOrDefault();
                if (staffLeave != null)
                {

                    staffLeave.StartDate = employeeLeaveViewModel.StartDate;
                    staffLeave.EndDate = employeeLeaveViewModel.EndDate;
                    staffLeave.LeaveReason = employeeLeaveViewModel.LeaveReason;
                    staffLeave.LeaveStatus = LeaveStatus.Applied;
                    _context.EmployeeLeaves.Update(staffLeave);
                    _context.SaveChanges();
                    return "Leave Successfully Updated";
                }
            }
            return "Leave failed To Update";
        }

		public List<EmployeeLeave> GetAllLeave(string currentAdminUsername)
		{
			var admin = FindByUserName(currentAdminUsername);
			var leave = _context.EmployeeLeaves.Where(x => x.Active == true && x.Deleted == false && x.LeaveStatus != LeaveStatus.Absence && x
			.StartDate.Year == DateTime.Now.Year).Include(s => s.User).ToList();
			leave = leave.OrderByDescending(x => x.DeteCreated).ToList();
			return leave;
		}



	}
}
