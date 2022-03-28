using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ITimesheetProjectRepository
    {
        Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjects();
        Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjectsByTeamId(long id);
        Task CreateTimesheetProject(TimesheetProject timesheetProject);
        Task<TimesheetProject> GetTimesheetProjectById(long id);
        void UpdateTimesheetProject(TimesheetProject timesheetProject);
        void DeleteTimesheetProject(TimesheetProject timesheetProject);
    }
}
