using AForge.Imaging;
using AForge.Imaging.Filters;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = System.Drawing.Image;

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
                var newAccount = new ApplicationUser
                {
                    FirstName = registrationData.FirstName,
                    LastName = registrationData.LastName,
                    Email = registrationData.Email,
                    UserName = registrationData.Email,
                    EmailConfirmed = true,
                    DisplayOnRota = registrationData.DisplayOnRota,
                    PhoneNumber = registrationData.Phone,
                    Address = registrationData.Address,
                    GenderId = registrationData.GenderId,
                    FaceImageData = registrationData.FaceImageData
                };

                // Hash the user's password and store it securely
                var result = await _userManager.CreateAsync(newAccount, registrationData.Password);

                if (result.Succeeded)
                {
                    return newAccount;
                }
                else
                {
                    // Log or handle registration errors here
                    foreach (var error in result.Errors)
                    {
                        // Log error messages, or handle them as needed
                    }
                }
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

        //public string GetUserDashboardPage(ApplicationUser userr)
        //{
        //    var userRole = _userManager.GetRolesAsync(userr).Result;

        //        if (userRole.Contains("SuperAdmin"))
        //        {
        //            return "/SuperAdmin/Dashboard";

        //        }
        //        else if (userRole.Contains ("CompanyAdmin"))
        //        {
        //            return "/Admin/Index";
        //        }
        //        else
        //        {
        //            return "/User/Index";
        //        }
        //         return null;
        //}

		public string GetUserDashboardPage(ApplicationUser user)
		{
			var userRole = _userManager.GetRolesAsync(user).Result;

			if (userRole.Contains("SuperAdmin"))
			{
				return "/SuperAdmin/Index";
			}
			else if (userRole.Contains("CompanyAdmin"))
			{
				return "/Admin/Index";
			}
			else if (userRole.Contains("CompanyStaff"))
			{
				return "/Staff/Index";
			}
			else if (userRole.Contains("User"))
			{
				return "/User/Index";
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
