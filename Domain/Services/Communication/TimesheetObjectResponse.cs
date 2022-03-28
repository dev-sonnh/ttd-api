using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class TimesheetObjectResponse : BaseResponse
    {
        public TimesheetObject TimesheetObject{ get; private set; }

        private TimesheetObjectResponse(bool success, string message, TimesheetObject timesheetCategory) : base(success, message)
        {
            TimesheetObject = timesheetCategory;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TimesheetObjectResponse(TimesheetObject timesheetCategory) : this(true, string.Empty, timesheetCategory)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TimesheetObjectResponse(string message) : this(false, message, null)
        { }
    }
}
