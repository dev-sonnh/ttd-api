using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ITimesheetCategoryService
    {
        Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategories();
        Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategoriesByTeamId(long id);
        Task<TimesheetCategory> GetTimesheetCategoryById(long id);
        Task<TimesheetCategoryResponse> SaveTimesheetCategory(TimesheetCategory timesheetCategory);
        Task<TimesheetCategoryResponse> UpdateTimesheetCategory(long id, TimesheetCategory timesheetCategory);
        Task<TimesheetCategoryResponse> DeleteTimesheetCategory(long id);
    }
}
