using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ITimesheetObjectService
    {
        Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjects();
        Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjectsByTeamId(long id);
        Task<TimesheetObject> GetTimesheetObjectById(long id);
        Task<TimesheetObjectResponse> SaveTimesheetObject(TimesheetObject timesheetObject);
        Task<TimesheetObjectResponse> UpdateTimesheetObject(long id, TimesheetObject timesheetObject);
        Task<TimesheetObjectResponse> DeleteTimesheetObject(long id);
    }
}
