using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ILeaveTypeRepository
    {
        Task<IEnumerable<LeaveType>> GetAllLeaveTypes();
        Task CreateLeaveType(LeaveType leaveType);
        Task<LeaveType> GetLeaveTypeById(long id);
        void UpdateLeaveType(LeaveType leaveType);
        void DeleteLeaveType(LeaveType leaveType);
    }
}
