﻿using Core.Db;
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
			//rotaToUpdate.RotaObject = JsonConvert.DeserializeObject<RotaObject[]>(rotaToUpdate.RotaObjectString);

			var rotaObjToUpdate = rotaToUpdate.RotaObjectGet.Where(v => v.Date == model.Date).FirstOrDefault();
			var index = Array.IndexOf(rotaToUpdate.RotaObjectGet, rotaObjToUpdate);
			rotaObjToUpdate.ShiftId = model.ShiftId;
			rotaObjToUpdate.TRange = GetTRange(model.ShiftId.Value);

			rotaToUpdate.RotaObjectGet[index] = rotaObjToUpdate;
			rotaToUpdate.RotaObjectString = JsonConvert.SerializeObject(rotaToUpdate.RotaObjectGet);

			_context.Update(rotaToUpdate);
			_context.SaveChanges();
		}

		public string GetTRange(int shiftId)
		{
			if (shiftId > 0)
			{
				var xxx = _context.shift.Where(v => v.Id == shiftId).FirstOrDefault();
				return xxx.StartTime + " - " + xxx.EndTime;
			}
			return null;
		}

		public List<string> GetRotaDateForGivenWeek(DateTime date)
		{
			if(date < DateTime.MinValue)
			{
				date = DateTime.Now;
			}
			var list = new List<string>();
			//var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
			var sunday = GetFirstDayOfWeek(date);
			for (int i = 0; i < 7; i++)
			{
				var datelist = ConvertDateToYYYYMMDD(sunday.AddDays(i));
				list.Add(datelist);
			}
			return list;
		}

		public DateTime GetFirstDayOfWeek(DateTime date)
		{
			int diff = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
			DateTime firstDay = date.AddDays(-diff).Date;
			return firstDay;
		}

		public StaffRota GetWeeklyStaffRota(string userId,DateTime date)
		{
			var result = new StaffRota();
			if(userId != null)
			{
				if(date == DateTime.MinValue)
				{
					date = DateTime.Now;
				}
				
				var dateIds = GetRotaDateForGivenWeek(date);
				var rota = _context.StaffRotas.Where(x => x.UserId == userId && x.Year == date.Year.ToString()).FirstOrDefault();
				if(rota != null)
				{
					var rotamodel = new List<RotaObject>();

					var ggg = rota.RotaObjectGet.Where(x => dateIds.Contains(x.Date));
					foreach (var item in ggg)
					{
						if(item.ShiftId >  0 && item.ShiftId != null)
						{
							item.shift = GetShiftById(item.ShiftId.Value);
						}
						rotamodel.Add(item);
					}
					result = rota;
					result.RotaObject = rotamodel.ToArray();
				}

			}

			return result;
		}

		public Shifts GetShiftById(int id)
		{
			if(id > 0)
			{
				return _context.shift.Where(a => a.Id == id).Include(c => c.Locations).FirstOrDefault();
			}
			return null;
		}
	}
}