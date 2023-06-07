﻿using Core.Models;
using Core.ViewModels;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
		Task<ApplicationUser> FindByEmailAsync(string email);
		ApplicationUser FindByUserName(string username);
		Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber);
		Task<ApplicationUser> FindByIdAsync(string Id);
		List<Location> GetLocations(string userName);
		bool AddLoction(LocationViewModel locationDetails, string userName);
		Location GetLocationById(int id, string userName);
		bool LocationEdited(LocationViewModel locationViewModel, string userName);
		bool DeleteLocation(int id);
		List<Department> GetDEepartments(string userName);
		bool AddDepartment(DepartmentViewModel departmentDetails, string userName);
		Department GetDepartmentById(int id, string userName);
		bool DepartmentEdited(DepartmentViewModel departmentViewModel, string userName);
		bool DeleteDepartment(int id);
		Task<ApplicationUser> FindByUserNameAsync(string userName);
		bool AddShift(DepartmentViewModel shiftDetails);


	}
}
