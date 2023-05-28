using Core.Models;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
		Task<ApplicationUser> FindByEmailAsync(string email);
	}
}
