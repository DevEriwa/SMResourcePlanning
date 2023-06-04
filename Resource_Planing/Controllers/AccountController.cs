using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.Helpers;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Buffers.Text;
using static Core.Enums.Resource_Planing;

namespace Resource_Planing.Controllers
{
	public class AccountController : Controller
    {
		private readonly IAccountHelper _accountHelper;
		private readonly IUserHelper _userHelper;
		private readonly IDropdownHelper _dropdownHelper;
		private readonly AppDbContext _context;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(IAccountHelper accountHelper, IUserHelper userHelper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext context, IDropdownHelper dropdownHelper)
		{
			_accountHelper = accountHelper;
			_userHelper = userHelper;
			_signInManager = signInManager;
			_userManager = userManager;
			_context = context;
			_dropdownHelper = dropdownHelper;
		}
		[HttpGet]
		public IActionResult Register()
        {
			ViewBag.Gender = _dropdownHelper.GetDropdownsByKey(DropdownEnums.Gender).Result;
			return View();
        }
        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

		[HttpPost]
		public async Task<JsonResult> Registeration(string userRegistrationData)
		{
			try
			{
				//var newAccountData = JsonConvert.DeserializeObject<UserViewModel>(userRegistrationData);
				if (userRegistrationData != null)
				{
					var newAccountData = JsonConvert.DeserializeObject<UserViewModel>(userRegistrationData);
					if (newAccountData != null)
					{
						var emailCheck = await _userHelper.FindByEmailAsync(newAccountData.Email).ConfigureAwait(false);
						if (emailCheck != null)
						{
							return Json(new { isError = true, msg = "Email already exist" });
						}
						
						if (newAccountData.Password != newAccountData.ConfirmPassword)
						{
							return Json(new { isError = true, msg = "Password and Confirm password does not match" });
						}
						var newAccountCreated = _accountHelper.AccountRegisterationService(newAccountData).Result;
						if (newAccountCreated != null)
						{
							var addToRole =  _userManager.AddToRoleAsync(newAccountCreated, "Staff").Result; 
							if (addToRole.Succeeded)
							{
								return Json(new { isError = false, msg = "Registration successful" });
							}
						}
						return Json(new { isError = true, msg = "Unable to create Company" });
					}
				}
				return Json(new { isError = true, msg = " Network failure, please try again." });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		// ADMIN REGISTRAION POST
		[HttpPost]
        public async Task<JsonResult> AdminRegisteration(string adminRegistrationData)
        {
            try
            {
                var newAccountData = JsonConvert.DeserializeObject<UserViewModel>(adminRegistrationData);
                if (newAccountData != null)
                {
                    var emailCheck = await _userHelper.FindByEmailAsync(newAccountData.Email);
                    if (emailCheck != null)
                    {
                        return Json(new { isError = true, msg = "Email already exist" });
                    }
                    else
                    {
                        var newAccountCreated = _accountHelper.AdminegisterationService(newAccountData).Result;
                        if (newAccountCreated != null)
                        {
                            var addToRole = _userManager.AddToRoleAsync(newAccountCreated, "Admin").Result;
                            if (addToRole.Succeeded)
                            {
                                return Json(new { isError = false, msg = "Registeration successful" });
                            }
                        }
                    }
                }
                return Json(new { isError = true, msg = "Reistration Failed" });
            }
            catch (Exception ex)
            {
                return Json(new { isError = true, msg = "Reistration Failed" + ex.Message });
            }
        }
    }
}
