using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ITimesheetRepository
    {
        Task<IEnumerable<Timesheet>> GetAllTimesheets();
        Task CreateTimesheet(Timesheet timesheet);
        Task InsertTimeSheetMonthly();
        Task<Timesheet> GetTimesheetById(long id);
        Task<Timesheet> GetValidTimesheetByOrderNoAndDate(int orderNo, DateTime date);
        void UpdateTimesheet(Timesheet timesheet);
        void DeleteTimesheet(Timesheet timesheet);
        void UpdateTimesheetByFingerprintMachine(Timesheet timesheet);
        Task UpdateTimesheetByFingerprintMachineMultipleAsync(IEnumerable<Timesheet> timesheet);
    }
}
