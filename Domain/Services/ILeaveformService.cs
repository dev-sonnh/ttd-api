using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ILeaveformService
    {
        Task<IEnumerable<Leaveform>> GetAllLeaveforms();
        Task<Leaveform> GetLeaveformById(long id);
        Task<IEnumerable<Leaveform>> GetLeaveformByUserRole(long id);
        Task<IEnumerable<Leaveform>> GetLeaveformByUserIdAndCurrentYear(long id);
        Task<LeaveformResponse> SaveLeaveform(Leaveform leaveform);
        Task<LeaveformResponse> UpdateLeaveform(long id, Leaveform leaveform);
        Task<LeaveformResponse> ChangeStatusOfRequest(long id, Leaveform leaveform);
        Task<LeaveformResponse> DeleteLeaveform(long id);
    }
}
