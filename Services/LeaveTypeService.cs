using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LeaveType>> GetAllLeaveTypes()
        {
            return await _leaveTypeRepository.GetAllLeaveTypes();
        }

        public async Task<LeaveType> GetLeaveTypeById(long id)
        {
            return await _leaveTypeRepository.GetLeaveTypeById(id);
        }

        public async Task<LeaveTypeResponse> SaveLeaveType(LeaveType leaveType)
        {
            try
            {
                await _leaveTypeRepository.CreateLeaveType(leaveType);

                return new LeaveTypeResponse(leaveType);
            }
            catch (Exception ex)
            {
                return new LeaveTypeResponse($"An error occurred when saving leaveType -.-! :{ex.Message}");
            }
        }

        public async Task<LeaveTypeResponse> UpdateLeaveType(long id, LeaveType leaveType)
        {
            var existingLeaveType = await _leaveTypeRepository.GetLeaveTypeById(id);

            if (existingLeaveType == null)
                return new LeaveTypeResponse("LeaveType is not found");

            existingLeaveType.Type = leaveType.Type;
            existingLeaveType.ModifiedBy = leaveType.ModifiedBy;

            try
            {
                _leaveTypeRepository.UpdateLeaveType(existingLeaveType);
                await _unitOfWork.CompleteAsync();

                return new LeaveTypeResponse(existingLeaveType);
            }
            catch (Exception ex)
            {
                return new LeaveTypeResponse($"An error occurred when updating leaveTypes: {ex.Message}");
            }
        }

        public async Task<LeaveTypeResponse> DeleteLeaveType(long id)
        {
            var existingLeaveType = await _leaveTypeRepository.GetLeaveTypeById(id);

            if (existingLeaveType == null)
                return new LeaveTypeResponse("LeaveType is not found");
            try
            {
                _leaveTypeRepository.DeleteLeaveType(existingLeaveType);
                await _unitOfWork.CompleteAsync();

                return new LeaveTypeResponse(existingLeaveType);
            }
            catch (Exception ex)
            {
                return new LeaveTypeResponse($"An error occurred when updating leaveTypes: {ex.Message}");
            }
        }
    }
}
