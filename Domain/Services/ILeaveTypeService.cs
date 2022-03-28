using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveType>> GetAllLeaveTypes();
        Task<LeaveType> GetLeaveTypeById(long id);
        Task<LeaveTypeResponse> SaveLeaveType(LeaveType leaveType);
        Task<LeaveTypeResponse> UpdateLeaveType(long id, LeaveType leaveType);
        Task<LeaveTypeResponse> DeleteLeaveType(long id);
    }
}
