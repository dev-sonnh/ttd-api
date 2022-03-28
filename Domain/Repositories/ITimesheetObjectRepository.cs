using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ITimesheetObjectRepository
    {
        Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjects();
        Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjectsByTeamId(long id);
        Task CreateTimesheetObject(TimesheetObject timesheetObject);
        Task<TimesheetObject> GetTimesheetObjectById(long id);
        void UpdateTimesheetObject(TimesheetObject timesheetObject);
        void DeleteTimesheetObject(TimesheetObject timesheetObject);
    }
}
