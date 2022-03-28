using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TimesheetService(ITimesheetRepository timesheetRepository, IUnitOfWork unitOfWork)
        {
            _timesheetRepository = timesheetRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Timesheet>> GetAllTimesheets()
        {
            return await _timesheetRepository.GetAllTimesheets();
        }

        public async Task<Timesheet> GetTimesheetById(long id)
        {
            return await _timesheetRepository.GetTimesheetById(id);
        }

        public async Task<TimesheetResponse> SaveTimesheet(Timesheet timesheet)
        {
            try
            {
                await _timesheetRepository.CreateTimesheet(timesheet);

                return new TimesheetResponse(timesheet);
            }
            catch (Exception ex)
            {
                return new TimesheetResponse($"An error occurred when saving timesheet -.-! :{ex.Message}");
            }
        }

        public async Task<TimesheetResponse> UpdateTimesheet(long id, Timesheet timesheet)
        {
            var existingTimesheet = await _timesheetRepository.GetTimesheetById(id);

            if (existingTimesheet == null)
                return new TimesheetResponse("Timesheet is not found");

            existingTimesheet.Date = timesheet.Date;
            existingTimesheet.TimeIn = timesheet.TimeIn;
            existingTimesheet.TimeOut = timesheet.TimeOut;
            existingTimesheet.Note = timesheet.Note;
            existingTimesheet.ModifiedBy = timesheet.ModifiedBy;
            existingTimesheet.ModifiedDate = timesheet.ModifiedDate;

            try
            {
                _timesheetRepository.UpdateTimesheet(existingTimesheet);
                await _unitOfWork.CompleteAsync();

                return new TimesheetResponse(existingTimesheet);
            }
            catch (Exception ex)
            {
                return new TimesheetResponse($"An error occurred when updating timesheets: {ex.Message}");
            }
        }

        public async Task<TimesheetResponse> DeleteTimesheet(long id)
        {
            var existingTimesheet = await _timesheetRepository.GetTimesheetById(id);

            if (existingTimesheet == null)
                return new TimesheetResponse("Timesheet is not found");
            try
            {
                _timesheetRepository.DeleteTimesheet(existingTimesheet);
                await _unitOfWork.CompleteAsync();

                return new TimesheetResponse(existingTimesheet);
            }
            catch (Exception ex)
            {
                return new TimesheetResponse($"An error occurred when updating timesheets: {ex.Message}");
            }
        }

        public async Task<TimesheetResponse> UpdateTimesheetByFingerprintMachine(Timesheet timesheet)
        {
            try
            {
                _timesheetRepository.UpdateTimesheetByFingerprintMachine(timesheet);
                await _unitOfWork.CompleteAsync();

                return new TimesheetResponse(timesheet);
            }
            catch (Exception ex)
            {
                return new TimesheetResponse($"An error occurred when updating timesheets: {ex.Message}");
            }
        }

        public async Task<Timesheet> GetValidTimesheetbyOrderNoAndDate(int orderNo, DateTime date)
        {
            return await _timesheetRepository.GetValidTimesheetByOrderNoAndDate(orderNo, date);
        }

        public async Task<TimesheetResponse> UpdateTimesheetByFingerprintMachineMultiple(IEnumerable<Timesheet> timesheet)
        {
            try
            {
                await _timesheetRepository.UpdateTimesheetByFingerprintMachineMultipleAsync(timesheet);

                return new TimesheetResponse(true, "");
            }
            catch (Exception ex)
            {
                return new TimesheetResponse($"An error occurred when updating timesheets: {ex.Message}");
            }
        }
    }
}
