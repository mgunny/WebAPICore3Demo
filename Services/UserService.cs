using AgileObjects.AgileMapper;
using EmpSubbieWebAPI.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Services
{
    public class UserService : IUserService
    {
        //private IDataRepository _dataRepository;
        private readonly PasswordHasher<ApplicationUser> _identityPasswordHasher;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Constructor
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _identityPasswordHasher = new PasswordHasher<ApplicationUser>();
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        // Check if account email address is verified
        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user?.EmailConfirmed == true ? true : false;
        }


        // Validate Users Credentials
        public async Task<(bool isValid, ApplicationUser user)> AuthenticateUserAsync(string email, string password)
        {
            // Check if User email address found in DB
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) { return (false, null); }

            // Ensure User is in the Admin Role Group           
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Where(r => r == "Admin") == null) { return (false, null); }

            // Ensure Login details are valid
            var result = _identityPasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed) { return (false, user); }

            // Login Credentials valid         
            return (true, user);
        }

        public async Task<List<string>> GetRolesForUserAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        // Get User Details
        public async Task<ApplicationUser> GetUserDetailsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<ApplicationUser> GetUserDetailsByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            //var siteUser = Mapper.Map(user).ToANew<SiteUser>();
            return user;
        }

        public async Task<IList<ApplicationUser>> GetUsersInRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users;
        }

    }
}
