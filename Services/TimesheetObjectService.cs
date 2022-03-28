using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class TimesheetObjectService : ITimesheetObjectService
    {
        private readonly ITimesheetObjectRepository _timesheetObjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TimesheetObjectService(ITimesheetObjectRepository timesheetObjectRepository, IUnitOfWork unitOfWork)
        {
            _timesheetObjectRepository = timesheetObjectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjects()
        {
            return await _timesheetObjectRepository.GetAllTimesheetObjects();
        }

        public async Task<TimesheetObject> GetTimesheetObjectById(long id)
        {
            return await _timesheetObjectRepository.GetTimesheetObjectById(id);
        }

        public async Task<TimesheetObjectResponse> SaveTimesheetObject(TimesheetObject timesheetObject)
        {
            try
            {
                await _timesheetObjectRepository.CreateTimesheetObject(timesheetObject);

                return new TimesheetObjectResponse(timesheetObject);
            }
            catch (Exception ex)
            {
                return new TimesheetObjectResponse($"An error occurred when saving timesheetObject -.-! :{ex.Message}");
            }
        }

        public async Task<TimesheetObjectResponse> UpdateTimesheetObject(long id, TimesheetObject timesheetObject)
        {
            var existingTimesheetObject = await _timesheetObjectRepository.GetTimesheetObjectById(id);

            if (existingTimesheetObject == null)
                return new TimesheetObjectResponse("TimesheetObject is not found");

            existingTimesheetObject.Name = timesheetObject.Name;
            existingTimesheetObject.IsPublic = timesheetObject.IsPublic;
            existingTimesheetObject.IsActive = timesheetObject.IsActive;
            existingTimesheetObject.ModifiedBy = timesheetObject.ModifiedBy;
            existingTimesheetObject.ModifiedDate = timesheetObject.ModifiedDate;

            try
            {
                _timesheetObjectRepository.UpdateTimesheetObject(existingTimesheetObject);
                await _unitOfWork.CompleteAsync();

                return new TimesheetObjectResponse(existingTimesheetObject);
            }
            catch (Exception ex)
            {
                return new TimesheetObjectResponse($"An error occurred when updating timesheetObjects: {ex.Message}");
            }
        }

        public async Task<TimesheetObjectResponse> DeleteTimesheetObject(long id)
        {
            var existingTimesheetObject = await _timesheetObjectRepository.GetTimesheetObjectById(id);

            if (existingTimesheetObject == null)
                return new TimesheetObjectResponse("TimesheetObject is not found");
            try
            {
                _timesheetObjectRepository.DeleteTimesheetObject(existingTimesheetObject);
                await _unitOfWork.CompleteAsync();

                return new TimesheetObjectResponse(existingTimesheetObject);
            }
            catch (Exception ex)
            {
                return new TimesheetObjectResponse($"An error occurred when updating timesheetObjects: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjectsByTeamId(long id)
        {
            return await _timesheetObjectRepository.GetAllTimesheetObjectsByTeamId(id);
        }
    }
}
