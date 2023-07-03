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
		private readonly IRotaHelper _rotaHelper;
        private readonly IAccountHelper _accountHelper;
		private readonly IUserHelper _userHelper;
		private readonly IDropdownHelper _dropdownHelper;
		private readonly AppDbContext _context;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(IAccountHelper accountHelper, IUserHelper userHelper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext context, IDropdownHelper dropdownHelper, IRotaHelper rotaHelper)
		{
			_accountHelper = accountHelper;
			_userHelper = userHelper;
			_signInManager = signInManager;
			_userManager = userManager;
			_context = context;
			_dropdownHelper = dropdownHelper;
			_rotaHelper = rotaHelper;
		}
		[HttpGet]
		public IActionResult Register()
        {
			ViewBag.Gender = _dropdownHelper.GetGenderDropDown();
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
                            _rotaHelper.CreateNewRotaObjectForUser(newAccountCreated, DateTime.Now.Year);
							var addToRole = _userManager.AddToRoleAsync(newAccountCreated, "CompanyStaff").Result;
                            if (addToRole.Succeeded)
                            {
                                return Json(new { isError = false, msg = "Registration successful" });
                            }
                        }
						return Json(new { isError = true, msg = "Unable to create Account" });
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
                            var addToRole = _userManager.AddToRoleAsync(newAccountCreated, "CompanyAdmin").Result;
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


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Login(string loginData)
        {
            var userDetails = JsonConvert.DeserializeObject<UserViewModel>(loginData);
            var user = await _userHelper.FindByEmailAsync(userDetails.Email);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    return Json(new { isNotVerified = true, msg = "Email Unverifed!!! Please Verify email to continue" });
                }
                else if (user.EmailConfirmed)
                {
                    var currentUser = _accountHelper.AuthenticateUser(userDetails).Result;
                    if (currentUser != null)
                    {
                        var dashboard = _accountHelper.GetUserDashboardPage(user);
                        return Json(new { isError = false, msg = "Welcome! " + currentUser.Name, dashboard = dashboard });
                    }
                }
            }
            return Json(new { isError = true, msg = "Login Failed" });
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
