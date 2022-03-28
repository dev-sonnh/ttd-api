using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class TimesheetTaskResponse : BaseResponse
    {
        public TimesheetTask TimesheetTask{ get; private set; }

        private TimesheetTaskResponse(bool success, string message, TimesheetTask timesheetTask) : base(success, message)
        {
            TimesheetTask = timesheetTask;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TimesheetTaskResponse(TimesheetTask timesheetTask) : this(true, string.Empty, timesheetTask)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TimesheetTaskResponse(string message) : this(false, message, null)
        { }
    }
}
