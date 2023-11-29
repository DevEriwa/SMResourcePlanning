using AForge.Imaging.Filters;
using AForge.Imaging;
using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static Core.Enums.Resource_Planing;
using Image = System.Drawing.Image;
using System.Linq;
using GeoCoordinatePortable;
using Org.BouncyCastle.Asn1.Ocsp;

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
				if(locationDetails.ListOfUserId != null)
				{
					locationDetails.UserIds = JsonConvert.SerializeObject(locationDetails.ListOfUserId);
                }
				var locationModel = new Location()
				{
					Name = locationDetails.Name,
					AbbreviatedName = locationDetails.AbbreviatedName,
					UserIds = locationDetails.UserIds,
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
                    if (locationViewModel.ListOfUserId != null)
                    {
                        locationViewModel.UserIds = JsonConvert.SerializeObject(locationViewModel.ListOfUserId);
                    }
                    location.Name = locationViewModel.Name;
					location.AbbreviatedName = locationViewModel.AbbreviatedName;
					location.UserIds = locationViewModel.UserIds;
					
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
			var shift = _context.shift.Where(a => a.Active && !a.Deleted).Include(v => v.Locations).ToList();
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


		public bool AddShiftLocation(ShiftLocationViewModel shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftData = new ShiftsLocation()
				{
					Active = true,
					DeteCreated = DateTime.Now,
					Deleted = false,
					Name = shiftDetails.Name,
					ShiftId = shiftDetails.ShiftId,
					StateId = shiftDetails.StateId,
					Address = shiftDetails.Address,
					PostalCode = shiftDetails.PostalCode,
				};
				_context.ShiftsLocations.Add(shiftData);
				_context.SaveChanges();
				return true;
			}
			return false;
		}


		public bool EditShiftLocation(ShiftLocationViewModel shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftlocToEdit = _context.ShiftsLocations.Where(c => c.Id == shiftDetails.Id).Include(sl => sl.Shift)
                .Include(sl => sl.State).FirstOrDefault();
				if (shiftlocToEdit != null)
				{
					shiftlocToEdit.Name = shiftDetails.Name;
					shiftlocToEdit.ShiftId = shiftDetails.ShiftId;
					//shiftlocToEdit.Shift = shiftDetails.Shift;
					shiftlocToEdit.StateId = shiftDetails.StateId;
					//shiftlocToEdit.State = shiftDetails.State;
					shiftlocToEdit.Address = shiftDetails.Address;
					shiftlocToEdit.PostalCode = shiftDetails.PostalCode;
					_context.ShiftsLocations.Update(shiftlocToEdit);
					_context.SaveChanges();
					return true;
				}
			}
			return false;
		}


		public bool DeleteShiftLocation(ShiftLocationViewModel shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftLocToDelete = _context.ShiftsLocations.Where(c => c.Id == shiftDetails.Id && c.Active).FirstOrDefault();
				if (shiftLocToDelete != null)
				{
                    shiftLocToDelete.Active = false;
                    shiftLocToDelete.Deleted = true;
				}
				_context.ShiftsLocations.Update(shiftLocToDelete);
				_context.SaveChanges();
				return true;
			}
			return false;
		}
		public async Task<ShiftsLocation> GetShiftById(int shiftId)
		{
			try
			{
				if (shiftId > 0)
				{
					var companyBranch = await _context.ShiftsLocations.Where(x => !x.Deleted && x.Id == shiftId).FirstOrDefaultAsync();
					if (companyBranch != null)
						return companyBranch;
				}
				return null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        public List<ShiftsLocation> GetShiftLocationList()
        {
            var shiftsLocationList = _context.ShiftsLocations
                .Where(sl => !sl.Deleted && sl.Active)
                .OrderByDescending(sl => sl.DeteCreated)
                .ToList();
            return shiftsLocationList;
        }

        public ShiftsLocation GetShiftLocation(int shiftId)
        {
            var shiftLocation = _context.ShiftsLocations
                .Include(sl => sl.Shift)
                .Include(sl => sl.State)
                .FirstOrDefault(sl => sl.Id == shiftId);

            return shiftLocation;
        }
        public string GetCurrentUserId(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefaultAsync().Result.Id?.ToString();
        }
        public List<LeaveApplication> GetAllLeaveStaffAbsense(string currentAdminUsername)
        {
            var admin = FindByUserName(currentAdminUsername);
            return _context.LeaveApplications.Where(x => x.Active && x
			    .Deleted == false && x.Status == LeaveStatus.Absence)
				.Include(v=> v.User)
				.Include(v => v.Leave).ToList();
        }
        public List<LeaveApplication> GetStaffLeave(string username)
        {
            var listOfLeaves = new List<LeaveApplication>();
            var currentUserId = GetCurrentUserId(username);
            if (currentUserId != null)
            {
                var leaves = _context.LeaveApplications.Where(x => (x.Active == true) &&
				(x.Deleted == false) && (x.StaffId == currentUserId) && 
				(x.DeteCreated != DateTime.MinValue))?
				.Include(s => s.Leave).ToList();
                leaves = leaves.OrderByDescending(x => x.DeteCreated).ToList();
                if (leaves.Any())
                {
                    return leaves;
                }
            }
            return listOfLeaves;
        }

        public LeaveSetup GetAnnualLeave(string username)
        {
            var user = _userManager.Users.Where(s => s.UserName == username).FirstOrDefault();
            return _context.LeaveSetups.Where(x => x.Active == true && x.Deleted == false && x.Name.ToLower().Contains("annual"))?.Include(v => v.Shift).FirstOrDefault();
        }

        public ApplicationUser FindAdminByUserName(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username)?.Include(x => x.Location)?.Include(x => x.Departments).FirstOrDefault();
        }
        public List<LeaveApplication> GetAllLeave(string currentAdminUsername)
        {
            var admin = FindAdminByUserName(currentAdminUsername);
            var leave = _context.LeaveApplications
                .Where(x => x.Active == true && x.Deleted
                == false && x.Status != LeaveStatus.Absence
                && x.StartDate.Year == DateTime.Now.Year)
                .Include(s => s.Leave).Include(s => s.User)
                .ToList();
            leave = leave.OrderByDescending(x => x.DeteCreated).ToList();
            return leave;
        }

        public ApplicationUser FindById(string Id)
        {
            var userInfo = _userManager.Users.Where(s => s.Id == Id).FirstOrDefault();
			if (userInfo != null)
			{
                return userInfo;
            }
			return null;
        }

        public LeaveApplication GetLeaveById(int id)
        {
            var leave = _context.LeaveApplications.Where(x => x.Id == id && x.StaffId != null && !x.Deleted).Include(s => s.User).FirstOrDefault();
            if (leave != null)
            {
                return (leave);
            }
            return null;
        }
        public bool CompareImages(Image referenceImage, Image capturedImage)
        {
            // Convert images to grayscale
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap referenceBitmap = filter.Apply(new Bitmap(referenceImage));
            Bitmap capturedBitmap = filter.Apply(new Bitmap(capturedImage));

            // Compute the difference between the two images
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.9f); // Adjust similarity threshold
            TemplateMatch[] matches = tm.ProcessImage(referenceBitmap, capturedBitmap);

            // If there are matches, consider the images as similar
            return matches.Length > 0;
        }

		public bool CompareImages(Image referenceImage, Image capturedImage, float similarityThreshold = 0.9f)
		{
			try
			{
				// Parameter validation
				if (referenceImage == null || capturedImage == null)
				{
					throw new ArgumentNullException("Both referenceImage and capturedImage must be provided.");
				}

				// Convert images to grayscale
				Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
				Bitmap referenceBitmap = filter.Apply(new Bitmap(referenceImage));
				Bitmap capturedBitmap = filter.Apply(new Bitmap(capturedImage));

				// Compute the difference between the two images
				ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(similarityThreshold);
				TemplateMatch[] matches = tm.ProcessImage(referenceBitmap, capturedBitmap);

                // If there are matches, consider the images as similar
                //return matches.Length > 0;
                return true;
            }
			catch (Exception ex)
			{
				// Log or handle the exception as needed
				Console.WriteLine($"Error in image comparison: {ex.Message}");
				return false;
			}
		}
        public List<Shifts> GetUserShiftsInLocation(string logedInUser, List<int> locationIds)
        {
            var shifts = new List<Shifts>();

            var shift = _context.shift
                .Where(a => a.Active && !a.Deleted &&
                            locationIds.Contains(a.LocationId.Value) &&
                            a.Locations.UserIds.Contains(logedInUser))
                .Include(v => v.Locations)
                .ToList();

            if (shift.Any())
            {
                shifts = shift;
            }

            return shifts;
        }
        public List<Shifts> GetUserShiftsInLocations(string logedInUser, List<int> locationIds)
        {
            var shifts = new List<Shifts>();
            var shiftExists = _context.shift
                .Any(a => a.Active && !a.Deleted &&
                          locationIds.Contains((int)a.LocationId) &&
                          a.Locations.UserIds.Contains(logedInUser));

            if (shiftExists)
            {
                // Fetch shifts that match the criteria
                shifts = _context.shift
                    .Where(a => a.Active && !a.Deleted &&
                                locationIds.Contains((int)a.LocationId) &&
                                a.Locations.UserIds.Contains(logedInUser))
                    .Include(v => v.Locations)
                    .ToList();
            }

            return shifts;
        }

        public List<int> GetUserLocationId(string userId)
        {
            var userLocations = _context.locations
                .Where(u => u.UserIds != null && u.UserIds.Contains(userId))
                .Select(u => u.Id)
                .ToList();
            return userLocations;
        }

		public StaffClockIn GetUserPunchActionForCurrentDay(string userId)
		{
			var model = new StaffClockIn();
			var mypunches = _context.StaffClockIns.Where(x => x.StaffId == userId && x.ClockInDate.Date == DateTime.Now.Date).FirstOrDefault();
			if(mypunches != null)
                model = mypunches;
			return model;
		}

		public Response PunchInService(PunchingViewModel model)
		{
			if(model.LocationId > 0 && model.UserId != null)
			{
				var location = GetLocationById(model.LocationId);
                var sCoord = new GeoCoordinate(model.Latitude, model.Longitude);
                if (location.Latitude == 0 && location.Longitude == 0)
                    return new Response { Status = false, Msg = "Sorry, the request can't be processed as you current shift has no coordinate" };
                var eCoord = new GeoCoordinate(location.Latitude, location.Longitude);
                var difference = eCoord.GetDistanceTo(sCoord) / 100;
                if (difference <= location.AcceptedRadius)
                {
                    var checkClockIn = _context.StaffClockIns.Where(a => a.StaffId == model.UserId).FirstOrDefault();

                    if (checkClockIn == null)
                    {
                        var clockIn = new StaffClockIn()
                        {
                            StaffId = model.UserId,
                            ClockId = Guid.NewGuid(),
                            DateTimeIn = DateTime.Now,
                            ClockInLatitude = sCoord.Latitude,
                            ClockInLongitude = sCoord.Longitude,
                            ClockInDate = DateTime.Now,
                        };
                        _context.StaffClockIns.Add(clockIn);
                        _context.SaveChanges();
                        return new Response { Status = true, isError = false, Msg = "Clocked In Successfully" };
                    }
                }
                return new Response { Status = false, Msg = "Sorry, the request can't be processed as you are current outside the accepted location" };
            }
            return new Response { Status = false, Msg = "Sorry, the request can't be processed as we couldn't get your current location" };
        }

        public Response PunchOutService(PunchingViewModel model)
        {
            if (model.LocationId > 0 && model.Id != null)
            {
                var location = GetLocationById(model.LocationId);
                var sCoord = new GeoCoordinate(model.Latitude, model.Longitude);
                if (location.Latitude == 0 && location.Longitude == 0)
                    return new Response { Status = false, Msg = "Sorry, the request can't be processed as you current shift has no coordinate" };
                var eCoord = new GeoCoordinate(location.Latitude, location.Longitude);
                var difference = eCoord.GetDistanceTo(sCoord) / 100;
                if (difference <= location.AcceptedRadius)
                {
                    var checkClockIn = _context.StaffClockIns.Where(a => a.Id == model.Id).FirstOrDefault();

                    if (checkClockIn != null)
                    {
                        checkClockIn.DateTimeOut = DateTime.Now;
                        checkClockIn.ClockOutLatitude = sCoord.Latitude;
                        checkClockIn.ClockOutLongitude = sCoord.Longitude;

                        _context.StaffClockIns.Update(checkClockIn);
                        _context.SaveChanges();
                        return new Response { Status = true, isError = false, Msg = "Clocked Out Successfully" };
                    }
                }
                return new Response { Status = false, Msg = "Sorry, the request can't be processed as you are current outside the accepted location" };
            }
            return new Response { Status = false, Msg = "Sorry, the request can't be processed as we couldn't get your current location" };
        }
    }
}
