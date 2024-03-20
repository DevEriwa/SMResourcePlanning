using Core.Db;
using Core.Models;
using Core.ViewModels;
using Core.ViewModels.Shift;
using Logic.IHelpers;
using Logic.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenCvSharp.Features2D;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
	public class RotaHelper: IRotaHelper
	{
		private readonly AppDbContext _context;
		private readonly IUserHelper _userHelper;
		private readonly IEmailServices _emailServices;

		public RotaHelper(AppDbContext context,
			IUserHelper userHelper,
			IEmailServices emailServices)
		{
			_context = context;
			_userHelper = userHelper;
			_emailServices = emailServices;
		}

		public int GetTotalDaysInYear(int year)
		{
			if (year > 0)
			{
				var isLeapyear = DateTime.IsLeapYear(year);
				if (isLeapyear)
				{ return 366; }
				return 365;
			}
			return 0;
		}

		public RotaObject[] GetYearlyRotaObject(int year)
		{
			int daysInCurrentYear = GetTotalDaysInYear(year);
			var firstDayofCurrentYear = new DateTime(year, 1, 1);
			var newRotaObj = new List<RotaObject>();
			for (int i = 0; i < daysInCurrentYear; i++)
			{
				var obj = new RotaObject();
				obj.Date = ConvertDateToYYYYMMDD(firstDayofCurrentYear.AddDays(i));
				newRotaObj.Add(obj);
			}
			return newRotaObj.ToArray();
		}

		public string ConvertDateToYYYYMMDD(DateTime date)
		{
			if (date != DateTime.MinValue)
			{
				return date.ToString("s").Split("T")[0];
			}
			return null;
		}

		public void CreateNewRotaObjectForUser(ApplicationUser model, int year)
		{
			if (model != null)
			{
				var isRotaCreated = _context.StaffRotas.Where(x => x.Year == year.ToString() && x.UserId == model.Id).Any();
				if (!isRotaCreated)
				{
					
					var data = new StaffRota()
					{
						
						UserId = model.Id,
						RotaObjectString = JsonConvert.SerializeObject(GetYearlyRotaObject(year)),
						Year = DateTime.Now.Year.ToString(),
						IsActive = true,
						DateCreated = DateTime.Now,
					};

					_context.StaffRotas.Add(data);
					_context.SaveChanges();
				}
			}
		}

		public void CreateRotaForNewYear()
		{
			var newYear = 1 + DateTime.Now.Year;
			var totalDaysInNewYear = GetTotalDaysInYear(newYear);
			var allStaffInRota = _context.ApplicationUser.Where(x => x.DisplayOnRota).ToList();
			foreach (var user in allStaffInRota)
			{
				CreateNewRotaObjectForUser(user, newYear);
			}
		}

        public bool UpdateRota(RotaObjectViewModel model)
         {
            var rotaToUpdate = _context.StaffRotas.Where(x => x.Year == model.Year && x.UserId == model.UserId).FirstOrDefault();

            if (rotaToUpdate == null)
            {
                return false; 
            }

            var newData = rotaToUpdate.RotaObjectGet?.Where(d => d.Date != "").ToList() ?? new List<RotaObject>();
            var dataToUpdate = new RotaObject
            {
                Date = model.Date,
				LocationId = model.LocationId,
                Location = model.Location,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                FixedAmount = model.FixedAmount,
                HourlyPay = model.HourlyPay,
                TRange = model.TRange,
                UnpaidTime = model.UnpaidTime,
            };

            newData.Add(dataToUpdate);
            newData.Sort((a, b) => a.Date.CompareTo(b.Date)); 
            rotaToUpdate.RotaObjectString = JsonConvert.SerializeObject(newData);
            _context.Update(rotaToUpdate);
            _context.SaveChanges();
			return true;
        }

		public bool UpdateRotas(RotaObjectViewModel model)
		{
			var existingRota = _context.StaffRotas
			.Include(x => x.User)
			.FirstOrDefault(x => x.Year == model.Year && x.UserId == model.UserId);
			if (existingRota == null)
			{
				existingRota = new StaffRota
				{
					Year = model.Year,
					UserId = model.UserId,
					RotaObjectString = "[]", 
					RotaObject = Array.Empty<RotaObject>(),
				};
				_context.StaffRotas.Add(existingRota);
			}
			var newShift = new RotaObject
			{
				Date = model.Date,
				LocationId = model.LocationId,
				Location = model.Location,
				StartTime = model.StartTime,
				EndTime = model.EndTime,
				FixedAmount = model.FixedAmount,
				HourlyPay = model.HourlyPay,
				TRange = model.TRange,
				UnpaidTime = model.UnpaidTime,
			};
			var newData = existingRota.RotaObjectGet?.Where(d => d.Date != "").ToList() ?? new List<RotaObject>();
			newData.Add(newShift);
			existingRota.RotaObjectString = JsonConvert.SerializeObject(newData);
			_context.SaveChanges();
			return true;
		}
		public Shifts GetShiftById(int id)
		{
			if(id > 0)
			{
				return _context.shift.Where(a => a.Id == id).Include(c => c.Locations).FirstOrDefault();
			}
			return null;
		}

		public TimeSpan AddTime(TimeSpan time1, TimeSpan time2)
		{
			return time2 - time1;
		}

		public string CoverTimeSpantoStringTime(TimeSpan time)
		{
			int totalHours = (int)time.TotalHours;
			int totalMinutes = time.Minutes;
			string totalHoursMinutesString = $"{totalHours}:{totalMinutes}";
			return totalHoursMinutesString; 
		}

		public RotaViewModel GenerateNewRota(DateTime sDate, DateTime eDate, int locId)
		{
			var model = new RotaViewModel();
			//Let get the DateList for the date range provide
			var dateList = GetDateRangeList(sDate, eDate);
			 
			//var daysOfTheWeekString = eDate.ToString("ddd");
			//int daysOfTheWeekInt = eDate.Day;
			model.RotaTableContainer = GenerateContent(dateList, locId);
			return model;
		}

        public string GenerateContentForUpdatedRota(RotaObjectViewModel data)
        {
            if (!string.IsNullOrEmpty(data.StartTime))
            {
                // If existing data is present, return existing data along with the plus sign
                return data.StartTime + " - " + data.EndTime +
                       "<span class=\"badge bg-success\"> " + data.Location + "</span><br/>" +
                       "<span><i class=\"fa fa-plus-circle\" onclick=\"popModal('" + data.Date + "','" + data.UserId + "','" + data.Year + "')\"></i></span>";
            }
            else
            {
                // If no existing data is present, return only the plus sign
                return "<span><i class=\"fa fa-plus-circle\" onclick=\"popModal('" + data.Date + "','" + data.UserId + "','" + data.Year + "')\"></i></span>";
            }
        }


        public List<DateTime> GetDateRangeList(DateTime sDate, DateTime eDate)
		{
			var dList = new List<DateTime>();
			TimeSpan duration = eDate - sDate;
			int totalDays = duration.Days;

			for (int i = 0; i < totalDays + 1; i++)
			{
				DateTime datelist = sDate.AddDays(i);
				dList.Add(datelist);
			}
			return dList;
		}


        public string GenerateContensdt(List<DateTime> data, int locId)
        {
            var usersInRota = GetUsersInRota(locId);
            if (usersInRota.Count == 0)
            {
                return null;
            }
            var dateIds = GetDateIdsForAGivenPeriod(data);
            var year = data.FirstOrDefault().Year;
            var thead = "<thead><tr><th class=\"p-1 text-center\">Users</th>";
            foreach (DateTime date in data)
            {
                var th = "<th class=\"p-1 text-center\">" + date.Day + "</th>";
                thead += th;
            }
            thead += "</tr></thead>";
            var tbody = "<tbody><tr><td  class=\"p-1\"></td>";
            foreach (DateTime date in data)
            {
                var td = "<td class=\"p-1 text-center\">" + date.ToString("ddd") + "</td>";
                tbody += td;
            }
            tbody += "</tr>";
            thead += tbody;
            var row = "";
            foreach (var user in usersInRota)
            {
                var userTD = "<td  class='p-1 text-center'>" + user.FirstName + " " + user.LastName + "</td>";
                //Get current user rota for the selected period
                var userRota = _context.StaffRotas.Where(x => x.UserId == user.Id && x.Year == year.ToString()).FirstOrDefault();
                if (userRota == null)
                {
                    CreateNewRotaObjectForUser(_userHelper.FindByIdAsync(user.Id).Result, year);
                    userRota = _context.StaffRotas.Where(x => x.UserId == user.Id && x.Year == year.ToString()).FirstOrDefault();
                }
                var rotamodel = new List<RotaObject>();

                var neededUserRota = userRota?.RotaObjectGet?.Where(x => dateIds.Contains(x.Date));
                var newTD = "";
                if (neededUserRota == null)
                {
                    return null;
                }
                foreach (var rot in neededUserRota)
                {
                    var td = "";
                    if (rot.Location != null)
                    {
                        td = "<td class=\"text-center p-1\" id='" + rot.Date + "_" + user.Id + "' onclick=\"popModal('" + rot.Date + "','" + user.Id + "','" + year + "')\">" + rot.TRange +
                            "<span class=\"badge bg-success\">" + rot?.Location + "</span></td>";
                    }
                    else
                    {
                        td = "<td class=\"text-center p-1\" id='" + rot.Date + "_" + user.Id + "' onclick=\"popModal('" + rot.Date + "','" + user.Id + "','" + year + "')\">" +
                             "<span><i class=\"fa fa-plus-circle\"></i></span></td>";
                    }

                    newTD += td;
                }
                var nonloopTD = "<input type=\"text\" id='" + user.Id + "' hidden value='" + user.Id + "' />";
                newTD += nonloopTD;
                row += "<tr>" + userTD + newTD + "</tr>";
            }
            return thead + row + "</tbody>";
        }

        public string GenerateContent(List<DateTime> data, int locId)
        {
            var usersInRota = GetUsersInRota(locId);
            if (usersInRota.Count == 0)
            {
                return null;
            }

            var dateIds = GetDateIdsForAGivenPeriod(data);
            var year = data.FirstOrDefault().Year;

            var thead = "<thead><tr><th class=\"p-1 text-center\">Users</th>";
            foreach (DateTime date in data)
            {
                var th = "<th class=\"p-1 text-center\">" + date.Day + "</th>";
                thead += th;
            }
            thead += "</tr></thead>";

            var tbody = "<tbody>";
            foreach (var user in usersInRota)
            {
                var userTD = "<td class='p-1 text-center'>" + user.FirstName + " " + user.LastName + "</td>";

                // Get current user rota for the selected period
                var userRota = _context.StaffRotas
                    .Where(x => x.UserId == user.Id && x.Year == year.ToString())
                    .FirstOrDefault();

                if (userRota == null)
                {
                    CreateNewRotaObjectForUser(_userHelper.FindByIdAsync(user.Id).Result, year);
                    userRota = _context.StaffRotas
                        .Where(x => x.UserId == user.Id && x.Year == year.ToString())
                        .FirstOrDefault();
                }

                var newTD = "";
                if (userRota != null)
                {
                    foreach (var date in data)
                    {
                        var dateFormats = new string[] { "yyyy-MM-dd", "dd/MM/yyyy HH:mm:ss" };
                        var shiftsForDate = userRota.RotaObjectGet
                            .Where(x => TryParseDate(x.Date, dateFormats, out DateTime parsedDate) && parsedDate == date && x.Location != null);

                        var td = "<td class=\"text-center p-1\" id='" + date + "_" + user.Id + "'>";

                        // Display existing data if available
                        foreach (var shift in shiftsForDate)
                        {
                            td += shift.TRange +
                                  "<span class=\"badge bg-success\">" + shift?.Location + "</span><br/>";
                        }

                        // Display a plus sign for adding shifts if no shifts exist for the date
                        td += "<span><i class=\"fa fa-plus-circle\" onclick=\"popModal('" + date + "','" + user.Id + "','" + year + "')\"></i></span>";

                        td += "</td>";

                        newTD += td;
                    }
                }

                var nonloopTD = "<input type=\"text\" id='" + user.Id + "' hidden value='" + user.Id + "' />";
                tbody += "<tr>" + userTD + newTD + nonloopTD + "</tr>";
            }
            tbody += "</tbody>";

            return thead + tbody;
        }
        private bool TryParseDate(string dateString, string[] dateFormats, out DateTime parsedDate)
		{
			foreach (var format in dateFormats)
			{
				if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
				{
					return true;
				}
			}

			parsedDate = default;
			return false;
		}
		public List<ApplicationUser> GetUsersInRota(int locId) 
		{
			var users = new List<ApplicationUser>();
			var location = _context.locations.Where(l => l.Id == locId).FirstOrDefault();
			if(location.UserIds != null)
			{
				var userIds = JsonConvert.DeserializeObject<List<String>>(location.UserIds);
				if (userIds.Any())
				{
					var list = _context.ApplicationUser.Where(a => userIds.Contains(a.Id)).ToList();
					if (list.Count > 0)
					{
						users = list;
					}
				}
			}
			return users;
		}
		public List<ApplicationUser> GetUsersInLocation(int locId)
		{
			var users = new List<ApplicationUser>();
			var location = _context.locations.FirstOrDefault(l => l.Id == locId);

			if (location != null && location.UserIds != null)
			{
				var userIds = JsonConvert.DeserializeObject<List<string>>(location.UserIds);

				if (userIds != null && userIds.Any())
				{
					users = _context.ApplicationUser.Where(u => userIds.Contains(u.Id)).ToList();
				}
			}
			return users;
		}

		public List<string> GetDateIdsForAGivenPeriod(List<DateTime> data)
		{
			var list = new List<string>();
			foreach (DateTime date in data)
			{
				var datelist = ConvertDateToYYYYMMDD(date);
				list.Add(datelist);
			}
			return list;
		}
        public List<string> GetDaysOfWeekForAGivenPeriodh(List<DateTime> data)
        {
            var list = new List<string>();
            foreach (DateTime date in data)
            {
                string dayOfWeek = date.DayOfWeek.ToString(); 
                list.Add(dayOfWeek);
            }
            return list;
        }

        public List<string> GetDaysOfWeekForAGivenPeriod(List<DateTime> data)
        {
            var list = new List<string>();
            foreach (DateTime date in data)
            {
                string dayOfWeek = date.DayOfWeek.ToString();
                // Convert the day names to start from Sunday (DayOfWeek.Sunday has value 0)
                int dayIndex = (int)date.DayOfWeek;
                string adjustedDayOfWeek = Enum.GetName(typeof(DayOfWeek), dayIndex);
                list.Add(adjustedDayOfWeek);
            }
            return list;
        }

        public bool UpdateLocation(int locationId, double latitude, double longitude, double acceptedRadius)
        {
            var clockInLocation = _context.locations.Find(locationId);
            if (clockInLocation != null)
            {
				clockInLocation.Id = locationId;
                clockInLocation.Longitude = longitude;
                clockInLocation.Latitude = latitude;
                clockInLocation.AcceptedRadius = acceptedRadius;
                _context.Update(clockInLocation);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public RotaObject GetUserRotaForCurrentDay(string userId)
        {
            var model = new RotaObject();
            var data = _context.StaffRotas.Where(x => x.UserId == userId && x.Year == DateTime.Now.Year.ToString()).FirstOrDefault();
			var currentDateString = ConvertDateToYYYYMMDD(DateTime.Now);
            if (data != null)
            {
                var schedule = data.RotaObjectGet.Where(x=>x.Date == currentDateString).FirstOrDefault();
				if (data != null)
					model = schedule;
            }
            return model;
        }

        public ClockInViewModel GetUserLoginVeiwDataForCurrentDay(string username)
        {
			var user = _context.ApplicationUser.Where(x=>x.UserName == username).FirstOrDefault();
            var model = new ClockInViewModel();
            model.rotaObject = GetUserRotaForCurrentDay(user.Id);
            model.PunchToday = _userHelper.GetUserPunchActionForCurrentDay(user.Id);
			model.Location = model.rotaObject == null ? "No Shift Assigned Today" : model.rotaObject.Location;
			model.ShiftStart = model.rotaObject == null ? "--:--" : AddAMPM(model.rotaObject.StartTime);
			model.ShiftEnd = model.rotaObject == null ? "--:--" : AddAMPM(model.rotaObject.EndTime);
			model.UserId = user.Id;

            if (model.PunchToday.StaffId == null) {
                model.ClockInTime = "--:--";
                model.ClockOutTime = "--:--";
			}
			else
			{
                model.ClockInTime = model.PunchToday.DateTimeIn.ToString("hh:mm tt");
                model.ClockOutTime = model.PunchToday.DateTimeOut == DateTime.MinValue ? "--:--" : model.PunchToday.DateTimeOut.ToString("hh:mm tt");
            }
            return model;
        }

        public static string AddAMPM(string time24Hour)
        {
            if (TimeSpan.TryParse(time24Hour, out TimeSpan time))
            {
                string ampm = (time.Hours < 12) ? "AM" : "PM";
                int hour12 = (time.Hours == 0 || time.Hours == 12) ? 12 : time.Hours % 12;
                return $"{hour12:D2}:{time.Minutes:D2}{ampm}";
            }
            else
            {
                return "N/A";
            }
        }
		public async Task<bool> ProcessUsersInLocationEmail(List<string> userIds)
		{
			try
			{
				foreach (var userId in userIds)
				{
					var user = await _context.Users.FindAsync(userId);

					if (user != null)
					{
						var locations = await _context.locations
							.Where(l => l.UserIds != null && l.UserIds.Contains(user.Id))
							.ToListAsync();

						if (locations.Any())
						{
							foreach (var location in locations)
							{
								var shifts = await _context.shift
									.Where(s => s.Active == true && s.LocationId == location.Id)
									.Include(s => s.Locations)
									.ToListAsync();

								if (shifts.Any())
								{
									foreach (var shift in shifts)
									{
										if (location != null && shift != null)
										{
											string receiverAddress = user.Email;
											string subject = "Shift Notification";
											string messageBody = $"Dear {user.UserName}, you are scheduled for a shift at {location.AbbreviatedName}. Please check the details.";

											// Call your email service to send the email
											 _emailServices.SendEmail(receiverAddress, subject, messageBody);
										}
									}
								}
							}
						}
					}
				}

				return true;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
        public string GenerateAdminContent(List<DateTime> data)
        {
            var usersLocationsInRota = GetAllUsersLocationsInRota();
            if (usersLocationsInRota.Count == 0)
            {
                return null;
            }
            var dateIds = GetDaysOfWeekForAGivenPeriod(data);
            var year = data.FirstOrDefault().Year;
            var thead = "<thead><tr><th class=\"p-1 text-center\">Employee</th>";

            // Populate table header for each date
            foreach (DateTime date in data)
            {
                var th = $"<th class=\"p-1 text-center\">{date.DayOfWeek} ({date.Day})</th>";
                thead += th;
            }

            // Populate table headers for Planned hrs and Actual hrs
            thead += "<th class=\"text-center\">Planned hrs</th><th class=\"text-center\">Actual hrs</th>";
            thead += "</tr></thead>";

            var tbody = "<tbody>";
            foreach (var userLocationsPair in usersLocationsInRota)
            {
                var user = userLocationsPair.Key;
                var locations = userLocationsPair.Value;

                var userTD = "<td class='p-1 text-center'>" + user.FirstName + " " + user.LastName + "</td>";

                // Assuming you still need to check year-wise rota for each user
                var userRota = _context.StaffRotas.FirstOrDefault(x => x.UserId == user.Id && x.Year == year.ToString());

                if (userRota == null)
                {
                    CreateNewRotaObjectForUser(_userHelper.FindByIdAsync(user.Id).Result, year);
                    userRota = _context.StaffRotas.FirstOrDefault(x => x.UserId == user.Id && x.Year == year.ToString());
                }

                var newTD = "";
                if (userRota != null)
                {
                    foreach (var date in data)
                    {
                        var dateFormats = new string[] { "yyyy-MM-dd", "dd/MM/yyyy HH:mm:ss" };
                        var shiftsForDate = userRota.RotaObjectGet.Where(x => TryParseDate(x.Date, dateFormats, out DateTime parsedDate) && parsedDate == date && x.Location != null);

                        var td = "<td class=\"text-center p-1\" id='" + date + "_" + user.Id + "'>";

                        // Display existing data if available
                        foreach (var shift in shiftsForDate)
                        {
                            td += shift.TRange + "<span class=\"badge bg-success\">" + shift?.Location + "</span><br/>";
                        }
                        td += "</td>";
                        newTD += td;
                    }
                }
                newTD += "<td class=\"text-center p-1\">" + GetPlannedHoursForUser(user.Id) + "</td>";
                newTD += "<td class=\"text-center p-1\">" + GetPlannedHoursForUser(user.Id) + "</td>";
                var nonloopTD = "<input type=\"text\" id='" + user.Id + "' hidden value='" + user.Id + "' />";
                tbody += "<tr>" + userTD + newTD + nonloopTD + "</tr>";
            }
            tbody += "</tbody>";
            return thead + tbody;
        }

        private string GetPlannedHoursForUser(string userId)
        {
			double totalHourlyPay = GetHourlyPayForUserInAllLocations(userId);
			return totalHourlyPay.ToString("0.00");
		}
		private double GetHourlyPayForUserInAllLocations(string userId)
		{
			double totalHourlyPay = 0.0;
			var userLocations = _context.locations
	        .Where(l => !string.IsNullOrEmpty(l.UserIds))
	        .ToList()
	        .Where(l =>
	        {
		        try
		        {
			        var userIds = JsonConvert.DeserializeObject<List<string>>(l.UserIds);
			        return userIds.Contains(userId);
		        }
		        catch (JsonReaderException ex)
		        {
			        // Log the error and capture the problematic JSON string
			        Console.WriteLine($"Error deserializing UserIds for Location ID {l.Id}: {ex.Message}");
			        Console.WriteLine($"Problematic JSON string: {l.UserIds}");
			        return false;
		        }
	        })
	        .ToList();


			// Iterate over each location
			foreach (var location in userLocations)
			{
				// Retrieve shifts in the current location
				var shiftsInLocation = _context.shift
					.Where(s => s.LocationId == location.Id)
					.ToList();

				// Check if the user is associated with this location
				if (location.UserIds != null && JsonConvert.DeserializeObject<List<string>>(location.UserIds).Contains(userId))
				{
					// Accumulate hourly pay for shifts in this location
					totalHourlyPay += shiftsInLocation.Sum(s => s.HourlyPay ?? 0.0);
				}
			}

			return totalHourlyPay;
		}
		private string GetActualHoursForUser(string userId)
        {
            // Add your logic to retrieve actual hours for the user from the database or elsewhere
            // For demonstration, returning a placeholder string
            return "ActualHrsFor" + userId;
        }

        public string GenerateAdminContentes(List<DateTime> data)
        {
            var usersLocationsInRota = GetAllUsersLocationsInRota();

            if (usersLocationsInRota.Count == 0)
            {
                return null;
            }

            var dateIds = GetDaysOfWeekForAGivenPeriod(data);
            var year = data.FirstOrDefault().Year;

            var thead = "<thead><tr><th class=\"p-1 text-center\">Employee</th>";
            foreach (DateTime date in data)
            {
                var th = $"<th class=\"p-1 text-center\">{date.DayOfWeek} ({date.Day})</th>";
                thead += th;
            }
            // Add the Planned hrs and Actual hrs table headers
            thead += "<th class=\"text-center\">Planned hrs</th><th class=\"text-center\">Actual hrs</th>";
            thead += "</tr></thead>";

            var tbody = "<tbody>";
            foreach (var userLocationsPair in usersLocationsInRota)
            {
                var user = userLocationsPair.Key;
                var locations = userLocationsPair.Value;

                var userTD = "<td class='p-1 text-center'>" + user.FirstName + " " + user.LastName + "</td>";

                // Assuming you still need to check year-wise rota for each user
                var userRota = _context.StaffRotas.FirstOrDefault(x => x.UserId == user.Id && x.Year == year.ToString());

                if (userRota == null)
                {
                    CreateNewRotaObjectForUser(_userHelper.FindByIdAsync(user.Id).Result, year);
                    userRota = _context.StaffRotas.FirstOrDefault(x => x.UserId == user.Id && x.Year == year.ToString());
                }

                var newTD = "";
                if (userRota != null)
                {
                    foreach (var date in data)
                    {
                        var dateFormats = new string[] { "yyyy-MM-dd", "dd/MM/yyyy HH:mm:ss" };
                        var shiftsForDate = userRota.RotaObjectGet.Where(x => TryParseDate(x.Date, dateFormats, out DateTime parsedDate) && parsedDate == date && x.Location != null);

                        var td = "<td class=\"text-center p-1\" id='" + date + "_" + user.Id + "'>";

                        // Display existing data if available
                        foreach (var shift in shiftsForDate)
                        {
                            td += shift.TRange + "<span class=\"badge bg-success\">" + shift?.Location + "</span><br/>";
                        }

                        // Display a plus sign for adding shifts if no shifts exist for the date
                        //td += "<span><i class=\"fa fa-plus-circle\" onclick=\"popModal('" + date + "','" + user.Id + "','" + year + "')\"></i></span>";

                        td += "</td>";

                        newTD += td;
                    }
                }

                var nonloopTD = "<input type=\"text\" id='" + user.Id + "' hidden value='" + user.Id + "' />";
                tbody += "<tr>" + userTD + newTD + nonloopTD + "</tr>";
            }
            tbody += "</tbody>";

            return thead + tbody;
        }
        public Dictionary<ApplicationUser, List<Location>> GetAllUsersLocationsInRota()
        {
            var usersLocations = new Dictionary<ApplicationUser, List<Location>>();
            var users = _context.ApplicationUser.ToList();

            foreach (var user in users)
            {
                var locations = _context.locations
                 .Where(l => !string.IsNullOrEmpty(l.UserIds))
                 .ToList() // Materialize the data
                 .Where(l =>
                 {
                     try
                     {
                         var userIds = JsonConvert.DeserializeObject<List<string>>(l.UserIds);
                         return userIds.Contains(user.Id);
                     }
                     catch (JsonReaderException ex)
                     {
                         return false;
                     }
                 })
                 .ToList();

                // Add the user and their associated locations to the dictionary
                usersLocations[user] = locations;
            }
            return usersLocations;
        }

    }
}
