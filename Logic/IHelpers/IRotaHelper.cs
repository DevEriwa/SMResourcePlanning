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
		bool UpdateRota(RotaObjectViewModel model);
		//StaffRota GetWeeklyStaffRota(string userId, DateTime date, int weekCount);
		RotaViewModel GenerateNewRota(DateTime sDate, DateTime eDate, int locId);
        Shifts GetShiftById(int id);
		bool UpdateLocation(int locationId, double latitude, double longitude, double acceptedRadius);
        RotaObject GetUserRotaForCurrentDay(string userId);
        ClockInViewModel GetUserLoginVeiwDataForCurrentDay(string username);
		List<ApplicationUser> GetUsersInLocation(int locId);
		Task<bool> ProcessUsersInLocationEmail(List<string> userIds);
		string GenerateContent(List<DateTime> data, int locId);
        string GenerateContentForUpdatedRota(RotaObjectViewModel data);
    }
}
