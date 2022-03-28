using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ITimesheetCategoryRepository
    {
        Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategories();
        Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategoriesByTeamId(long id);
        Task CreateTimesheetCategory(TimesheetCategory timesheetCategory);
        Task<TimesheetCategory> GetTimesheetCategoryById(long id);
        void UpdateTimesheetCategory(TimesheetCategory timesheetCategory);
        void DeleteTimesheetCategory(TimesheetCategory timesheetCategory);
    }
}
