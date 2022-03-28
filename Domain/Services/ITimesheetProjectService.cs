using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ITimesheetProjectService
    {
        Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjects();
        Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjectsByTeamId(long id);
        Task<TimesheetProject> GetTimesheetProjectById(long id);
        Task<TimesheetProjectResponse> SaveTimesheetProject(TimesheetProject timesheetProject);
        Task<TimesheetProjectResponse> UpdateTimesheetProject(long id, TimesheetProject timesheetProject);
        Task<TimesheetProjectResponse> DeleteTimesheetProject(long id);
    }
}
