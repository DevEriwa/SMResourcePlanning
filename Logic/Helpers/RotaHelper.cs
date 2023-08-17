using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
	public class RotaHelper: IRotaHelper
	{
		private readonly AppDbContext _context;
		private readonly IUserHelper _userHelper;

		public RotaHelper(AppDbContext context, IUserHelper userHelper)
		{
			_context = context;
			_userHelper = userHelper;
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

        public void UpdateRota(RotaObjectViewModel model)
        {
            var rotaToUpdate = _context.StaffRotas.Where(x => x.Year == model.Year && x.UserId == model.UserId).FirstOrDefault();

            if (rotaToUpdate == null)
            {
                return; // Handle the case where no rota is found
            }

            var newData = rotaToUpdate.RotaObjectGet?.Where(d => d.Date != model.Date).ToList() ?? new List<RotaObject>();

            var dataToUpdate = new RotaObject
            {
                Date = model.Date,
                Location = model.Location,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                FixedAmount = model.FixedAmount,
                HourlyPay = model.HourlyPay,
                TRange = model.TRange,
                UnpaidTime = model.UnpaidTime,
            };

            newData.Add(dataToUpdate);
            newData.Sort((a, b) => a.Date.CompareTo(b.Date)); // Sort the list by date

            rotaToUpdate.RotaObjectString = JsonConvert.SerializeObject(newData);
            _context.Update(rotaToUpdate);
            _context.SaveChanges();
        }

        //public StaffRota GetWeeklyStaffRota(string userId,DateTime date, int weekCount)
        //{
        //	var result = new StaffRota();
        //	if(userId != null)
        //	{
        //		if(date == DateTime.MinValue)
        //		{
        //			date = DateTime.Now;
        //		}
        //		if(weekCount > 0)
        //		{
        //			date = date.AddDays((weekCount * 7));
        //		}

        //		var dateIds = GetRotaDateForGivenWeek(date);
        //		var rota = _context.StaffRotas.Where(x => x.UserId == userId && x.Year == date.Year.ToString()).FirstOrDefault();
        //		if(rota == null)
        //		{
        //                  CreateNewRotaObjectForUser(_userHelper.FindByIdAsync(userId).Result, date.Year);
        //                  rota = _context.StaffRotas.Where(x => x.UserId == userId && x.Year == date.Year.ToString()).FirstOrDefault();
        //              }
        //              if (rota != null)
        //		{
        //			var rotamodel = new List<RotaObject>();

        //			var ggg = rota.RotaObjectGet.Where(x => dateIds.Contains(x.Date));
        //			TimeSpan sumTimeSpan = new TimeSpan();

        //			foreach (var item in ggg)
        //			{
        //				if(item.ShiftId >  0 && item.ShiftId != null)
        //				{
        //					item.shift = GetShiftById(item.ShiftId.Value);
        //					sumTimeSpan = sumTimeSpan +  AddTime(TimeSpan.Parse(item.shift.StartTime), TimeSpan.Parse(item.shift.EndTime));
        //				}
        //				rotamodel.Add(item);
        //			}
        //			rota.DateRange = dateIds[0] + " - " + dateIds[6];
        //			rota.TotalPlannedHour = CoverTimeSpantoStringTime(sumTimeSpan);
        //                  result = rota;
        //			result.RotaObject = rotamodel.ToArray();
        //		}
        //	}
        //	return result;
        //}

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

		public RotaViewModel GenerateNewRota(DateTime sDate, DateTime eDate)
		{
			var model = new RotaViewModel();
			//Let get the DateList for the date range provide
			var dateList = GetDateRangeList(sDate, eDate);
			 
			//var daysOfTheWeekString = eDate.ToString("ddd");
			//int daysOfTheWeekInt = eDate.Day;
			model.RotaTableContainer = GenerateContent(dateList);
			return model;
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

		public string GenerateContent(List<DateTime> data)
		{
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
			var usersInRota = GetUsersInRota();

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
				if(neededUserRota == null)
				{
					return null;
				}
				foreach (var rot in neededUserRota)
				{
					var td = "";
					if(rot.Location != null)
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

		public List<ApplicationUser> GetUsersInRota() 
		{ 
			var users = new List<ApplicationUser>();
			var list = _context.ApplicationUser.Where(a => a.DisplayOnRota).ToList();
			if(list.Count > 0)
			{
				users = list;
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
	}
}
