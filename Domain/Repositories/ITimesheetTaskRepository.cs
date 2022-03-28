using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ITimesheetTaskRepository
    {
        Task<IEnumerable<TimesheetTask>> GetAllTimesheetTasks();
        Task CreateTimesheetTask(TimesheetTask timesheetTask);
        Task<TimesheetTask> GetTimesheetTaskById(long id);
        void UpdateTimesheetTask(TimesheetTask timesheetTask);
        void DeleteTimesheetTask(TimesheetTask timesheetTask);
    }
}
