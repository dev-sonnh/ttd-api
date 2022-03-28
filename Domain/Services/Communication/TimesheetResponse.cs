using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class TimesheetResponse : BaseResponse
    {
        public Timesheet Timesheet{ get; private set; }

        private TimesheetResponse(bool success, string message, Timesheet timesheet) : base(success, message)
        {
            Timesheet = timesheet;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TimesheetResponse(Timesheet timesheet) : this(true, string.Empty, timesheet)
        { }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TimesheetResponse(bool success, string message) : this(success, message, null)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TimesheetResponse(string message) : this(false, message, null)
        { }
    }
}
