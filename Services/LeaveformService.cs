using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class LeaveformService : ILeaveformService
    {
        private readonly ILeaveformRepository _leaveformRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveformService(ILeaveformRepository leaveformRepository,
            IUnitOfWork unitOfWork)
        {
            _leaveformRepository = leaveformRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Leaveform>> GetAllLeaveforms()
        {
            return await _leaveformRepository.GetAllLeaveforms();
        }

        public async Task<Leaveform> GetLeaveformById(long id)
        {
            return await _leaveformRepository.GetLeaveformById(id);
        }

        public async Task<LeaveformResponse> SaveLeaveform(Leaveform leaveform)
        {
            try
            {
                await _leaveformRepository.CreateLeaveform(leaveform);

                return new LeaveformResponse(leaveform);
            }
            catch (Exception ex)
            {
                return new LeaveformResponse($"An error occurred when saving leaveform -.-! :{ex.Message}");
            }
        }

        public async Task<LeaveformResponse> UpdateLeaveform(long id, Leaveform leaveform)
        {
            var existingLeaveform = await _leaveformRepository.GetLeaveformById(id);

            if (existingLeaveform == null)
                return new LeaveformResponse("Leaveform is not found");

            existingLeaveform.LeaveTypeId = leaveform.LeaveTypeId;
            existingLeaveform.FromDate = leaveform.FromDate;
            existingLeaveform.ToDate = leaveform.ToDate;
            existingLeaveform.Status = leaveform.Status;
            existingLeaveform.Hours = leaveform.Hours;
            existingLeaveform.Reason = leaveform.Reason;
            existingLeaveform.ModifiedBy = leaveform.ModifiedBy;

            try
            {
                _leaveformRepository.UpdateLeaveform(existingLeaveform);
                await _unitOfWork.CompleteAsync();

                return new LeaveformResponse(existingLeaveform);
            }
            catch (Exception ex)
            {
                return new LeaveformResponse($"An error occurred when updating leaveforms: {ex.Message}");
            }
        }

        public async Task<LeaveformResponse> DeleteLeaveform(long id)
        {
            var existingLeaveform = await _leaveformRepository.GetLeaveformById(id);

            if (existingLeaveform == null)
                return new LeaveformResponse("Leaveform is not found");
            try
            {
                _leaveformRepository.DeleteLeaveform(existingLeaveform);
                await _unitOfWork.CompleteAsync();

                return new LeaveformResponse(existingLeaveform);
            }
            catch (Exception ex)
            {
                return new LeaveformResponse($"An error occurred when updating leaveforms: {ex.Message}");
            }
        }

        public async Task<LeaveformResponse> ChangeStatusOfRequest(long id, Leaveform leaveform)
        {
            var existingLeaveform = await _leaveformRepository.GetLeaveformById(id);

            if (existingLeaveform == null)
                return new LeaveformResponse("Leaveform is not found");

            existingLeaveform.Status = leaveform.Status.ToLower();
            existingLeaveform.ModifiedBy = leaveform.ModifiedBy;

            try
            {
                _leaveformRepository.ChangeStatusOfRequest(existingLeaveform);
                await _unitOfWork.CompleteAsync();

                return new LeaveformResponse(existingLeaveform);
            }
            catch (Exception ex)
            {
                return new LeaveformResponse($"An error occurred when updating leaveforms: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Leaveform>> GetLeaveformByUserRole(long id)
        {
            return await _leaveformRepository.GetLeaveformByUserRole(id);
        }

        public async Task<IEnumerable<Leaveform>> GetLeaveformByUserIdAndCurrentYear(long id)
        {
            return await _leaveformRepository.GetLeaveformByUserIdAndCurrentYear(id);
        }
    }
}
