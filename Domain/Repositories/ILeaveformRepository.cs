using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ILeaveformRepository
    {
        Task<IEnumerable<Leaveform>> GetAllLeaveforms();
        Task CreateLeaveform(Leaveform leaveform);
        Task<Leaveform> GetLeaveformById(long id);
        Task<IEnumerable<Leaveform>> GetLeaveformByUserRole(long id);
        Task<IEnumerable<Leaveform>> GetLeaveformByUserIdAndCurrentYear(long id);
        void UpdateLeaveform(Leaveform leaveform);
        void ChangeStatusOfRequest(Leaveform leaveform);
        void DeleteLeaveform(Leaveform leaveform);
    }
}
