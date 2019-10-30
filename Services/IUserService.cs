using EmpSubbieWebAPI.Data.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Services
{
    public interface IUserService
    {
        Task<(bool isValid, ApplicationUser user)> AuthenticateUserAsync(string email, string password);
        Task<List<string>> GetRolesForUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserDetailsAsync(string email);
        Task<ApplicationUser> GetUserDetailsByIdAsync(string userId);
        Task<IList<ApplicationUser>> GetUsersInRole(string roleName);
        Task<bool> IsEmailConfirmedAsync(string email);
    }
}