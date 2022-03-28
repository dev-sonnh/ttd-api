using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class LeaveTypeResponse : BaseResponse
    {
        public LeaveType LeaveType{ get; private set; }

        private LeaveTypeResponse(bool success, string message, LeaveType leaveType) : base(success, message)
        {
            LeaveType = leaveType;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public LeaveTypeResponse(LeaveType leaveType) : this(true, string.Empty, leaveType)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public LeaveTypeResponse(string message) : this(false, message, null)
        { }
    }
}
