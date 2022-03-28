using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ITimesheetTaskService
    {
        Task<IEnumerable<TimesheetTask>> GetAllTimesheetTasks();
        Task<TimesheetTask> GetTimesheetTaskById(long id);
        Task<TimesheetTaskResponse> SaveTimesheetTask(TimesheetTask timesheetTask);
        Task<TimesheetTaskResponse> UpdateTimesheetTask(long id, TimesheetTask timesheetTask);
        Task<TimesheetTaskResponse> DeleteTimesheetTask(long id);
    }
}
