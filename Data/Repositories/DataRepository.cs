using AgileObjects.AgileMapper;
using EmpSubbieWebAPI.Data.Contexts;
using EmpSubbieWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Data.Repositories
{
    public partial class DataRepository : IDataRepository
    {
        private readonly DataAccessContext _ctx;

        public DataRepository(DataAccessContext context)
        {
            _ctx = context;
        }


        #region User Forms

        public async Task<List<FormForUser>> GetFormsForUserAsync(string userId)
        {
            var data = await _ctx.FormForUser.FromSqlInterpolated($"dbo.spGet_FormsForUser {userId}").ToListAsync();
            return data;
        }


        public async Task<FormForUser> GetFormForUserAsync(string userId, int formId)
        {
            var data = await _ctx.FormForUser.FromSqlInterpolated($"dbo.spGet_FormForUser {userId}, {formId}").ToListAsync();
            return data.FirstOrDefault();
        }


        //public async Task<MaintenanceLog> GetMaintenanceLogAsync(int id)
        //{
        //    MaintenanceLog log = null;
        //    //var data = _db.CMS_spGet_MaintenanceLog(id).FirstOrDefault();

        //    var data = await _ctx.MaintenanceLogs.FromSql($"dbo.CMS_spGet_MaintenanceLog {id}").FirstOrDefaultAsync();

        //    if (data != null) { log = Mapper.Map(data).ToANew<MaintenanceLog>(); }

        //    return log;
        //}

        //public async Task UpdateMaintenanceLogAsync(MaintenanceLog m)
        //{
        //    //_db.CMS_spUpdate_MaintenanceLog(model.Id, model.LogId, model.LogDate, model.Title, model.Details,
        //    //                               model.RoleName, model.PlotNo, model.Name, model.Address,
        //    //                               model.BookedDate, model.BookedTime, model.Status, model.Invoiced);

        //   await _ctx.Database.ExecuteSqlCommandAsync($"dbo.CMS_spUpdate_MaintenanceLog {m.Id}, {m.LogId}, {m.LogDate}, {m.Title}, {m.Details}, {m.RoleName}, {m.PlotNo}, {m.Name}, {m.Address}, {m.BookedDate}, {m.BookedTime}, {m.Status}, {m.Invoiced}");
        //}

        //public async Task<List<MaintenanceLog>> GetLatestUpdatedMaintenanceLogsAsync()
        //{
        //    //var data = _db.API_spGet_LatestUpdatedMaintenanceLogs();
        //    var data = await _ctx.MaintenanceLogs.FromSql($"dbo.API_spGet_LatestUpdatedMaintenanceLogs").ToListAsync();

        //    var logs = Mapper.Map(data).ToANew<List<MaintenanceLog>>();
        //    return logs;
        //}

        //public async Task<List<MaintenanceLog>> GetUpcomingBookedAppointmentsAsync()
        //{

        //    var data = await _ctx.MaintenanceLogs.FromSql($"dbo.API_spGet_UpcomingBookedAppointments").ToListAsync();

        //    var logs = Mapper.Map(data).ToANew<List<MaintenanceLog>>();
        //    return logs;
        //}

        //public async Task<int> GetLatestUpdatedMaintenanceLogsCountAsync()
        //{
        //    //var data = _db.API_spGet_LatestUpdatedMaintenanceLogCount().FirstOrDefault().LogCount;

        //    var data = await _ctx.MaintenanceLogCount.FromSql($"dbo.API_spGet_LatestUpdatedMaintenanceLogCount").FirstOrDefaultAsync();

        //    return data.LogCount;
        //}

        #endregion

    }
}
