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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserHelper _userHelper;
		private readonly UserManager<ApplicationUser> _userManager;

        public AccountHelper(IUserHelper userHelper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userHelper = userHelper;
            _userManager = userManager;
            _signInManager = signInManager;
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
					newAccount.DisplayOnRota = registrationData.DisplayOnRota;
					newAccount.PhoneNumber = registrationData.Phone;
					newAccount.Password = registrationData.Password;
					newAccount.ConfirmPassword = registrationData.ConfirmPassword;
					newAccount.Address = registrationData.Address;
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


        public string GetUserDashboardPage(ApplicationUser userr)
        {
            var userRole = _userManager.GetRolesAsync(userr).Result;

                if (userRole.Contains("SuperAdmin"))
                {
                    return "/SuperAdmin/Dashboard";

                }
                else if (userRole.Contains ("CompanyAdmin"))
                {
                    return "/Admin/Dashboard";
                }
                else
                {
                    return "/Account/Login";
                }
                 return null;
        }

        public string GetUserLayout(string username)
        {
            var accountType = _userHelper.FindByUserNameAsync(username).Result;
            var userRole = _userManager.GetRolesAsync(accountType).Result.FirstOrDefault();
            if (userRole != null)
            {
                if (userRole == "Admin")
                {
                    return "~/Views/Shared/_AdminLayout.cshtml";
                }
                else
                {
                    return "~/Views/Shared/_InvestorLayout.cshtml";
                }
            }
            return null;
        }

        public async Task<ApplicationUser> AuthenticateUser(UserViewModel loginDetail)
        {
            var user = await _userManager.FindByEmailAsync(loginDetail.Email);
            if (user != null)
            {
                var logger = _signInManager.PasswordSignInAsync(user.UserName, loginDetail.Password, true, false).Result;
                if (logger.Succeeded)
                {
                    return user;
                }
            }
            return null;
        }


    }
}
