using Core.Db;
using Core.Models;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logic.Helpers
{
    public class UserHelper : IUserHelper
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;

        public UserHelper(AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            return await _userManager.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users.Where(s => s.PhoneNumber == phoneNumber)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.Where(s => s.Email == email)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindByIdAsync(string Id)
        {
            return await _userManager.Users.Where(s => s.Id == Id)?.FirstOrDefaultAsync();
        }
    }
}
