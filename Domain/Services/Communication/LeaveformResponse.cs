using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class LeaveformResponse : BaseResponse
    {
        public Leaveform Leaveform{ get; private set; }

        private LeaveformResponse(bool success, string message, Leaveform leaveform) : base(success, message)
        {
            Leaveform = leaveform;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public LeaveformResponse(Leaveform leaveform) : this(true, string.Empty, leaveform)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public LeaveformResponse(string message) : this(false, message, null)
        { }
    }
}
