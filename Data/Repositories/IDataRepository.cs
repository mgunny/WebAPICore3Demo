using System.Collections.Generic;
using System.Threading.Tasks;
using EmpSubbieWebAPI.Data.Entities;

namespace EmpSubbieWebAPI.Data.Repositories
{
    public interface IDataRepository
    {
        Task<List<FormForUser>> GetFormsForUserAsync(string userId);
        Task<FormForUser> GetFormForUserAsync(string userId, int formId);
    }
}