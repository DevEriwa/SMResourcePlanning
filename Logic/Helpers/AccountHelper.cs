using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class AccountHelper : IAccountHelper
    {
		private readonly IUserHelper _userHelper;
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountHelper(IUserHelper userHelper, UserManager<ApplicationUser> userManager)
		{
			_userHelper = userHelper;
			_userManager = userManager;
		}

		public async Task<ApplicationUser> AccountRegisterationService(UserViewModel registrationData)
		{
			if (registrationData != null)
			{
				var newAccount = new ApplicationUser();
				{
					newAccount.FirstName = registrationData.FirstName;
					newAccount.LastName = registrationData.LastName;
					newAccount.Email = registrationData.Email;
					newAccount.UserName = registrationData.Email;
					newAccount.EmailConfirmed = true;
					newAccount.StartDate = DateTime.Now;
					newAccount.Status = registrationData.Status;
					newAccount.MaritalStatus = registrationData.MaritalStatus;
					newAccount.PhoneNumber = registrationData.Phone;
					newAccount.DateOfBirth = registrationData.DateOfBirth;
					newAccount.Password = registrationData.Password;
					newAccount.ConfirmPassword = registrationData.ConfirmPassword;
					newAccount.Address = registrationData.Address;
					newAccount.Age = registrationData.Age;
					//newAccount.DepartmentId = registrationData.DepartmentId;
					newAccount.GenderId = registrationData.GenderId;
				}
				var result = await _userManager.CreateAsync(newAccount, registrationData.Password);
				if (result.Succeeded)
				{
					return newAccount;
				}
				return null;
			}
			return null;

		}
		public async Task<ApplicationUser> AdminegisterationService(UserViewModel registrationData)
		{
			if (registrationData != null)
			{
				var newAccount = new ApplicationUser();
				{
					newAccount.FirstName = registrationData.FirstName;
					newAccount.LastName = registrationData.LastName;
					newAccount.Email = registrationData.Email;
					newAccount.UserName = registrationData.Email;
					newAccount.EmailConfirmed = true;
					newAccount.StartDate = DateTime.Now;
					newAccount.Status = registrationData.Status;
					newAccount.MaritalStatus = registrationData.MaritalStatus;
					newAccount.PhoneNumber = registrationData.Phone;
					newAccount.DateOfBirth = registrationData.DateOfBirth;
					newAccount.Password = registrationData.Password;
					newAccount.ConfirmPassword = registrationData.ConfirmPassword;
				}
				var result = await _userManager.CreateAsync(newAccount, registrationData.Password);
				if (result.Succeeded)
				{
					return newAccount;
				}
				return null;
			}
			return null;

		}

	}
}
