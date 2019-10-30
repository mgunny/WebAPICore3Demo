using EmpSubbieWebAPI.Data.Contexts;
using EmpSubbieWebAPI.Models;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Services
{
    public interface IJWTService
    {
        Task<AccessTokenResponse> GenerateJWTokenResponseAsync(ApplicationUser userIdentity);
       
    }
}