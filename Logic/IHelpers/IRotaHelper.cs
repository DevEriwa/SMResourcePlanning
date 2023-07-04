using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
	public interface IRotaHelper
	{
		int GetTotalDaysInYear(int year);
		RotaObject[] GetYearlyRotaObject(int year);
		string ConvertDateToYYYYMMDD(DateTime date);
		void CreateNewRotaObjectForUser(ApplicationUser model, int year);
		void UpdateRota(RotaObjectViewModel model);
		string GetTRange(int shiftId);
		StaffRota GetWeeklyStaffRota(string userId, DateTime date);
	}
}
