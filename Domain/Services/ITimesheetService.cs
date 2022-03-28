using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ITimesheetService
    {
        Task<IEnumerable<Timesheet>> GetAllTimesheets();
        Task<Timesheet> GetTimesheetById(long id);
        Task<Timesheet> GetValidTimesheetbyOrderNoAndDate(int orderNo, DateTime date);
        Task<TimesheetResponse> SaveTimesheet(Timesheet timesheet);
        Task<TimesheetResponse> UpdateTimesheet(long id, Timesheet timesheet);
        Task<TimesheetResponse> DeleteTimesheet(long id);
        Task<TimesheetResponse> UpdateTimesheetByFingerprintMachine(Timesheet timesheet);
        Task<TimesheetResponse> UpdateTimesheetByFingerprintMachineMultiple(IEnumerable<Timesheet> timesheet);
    }
}
