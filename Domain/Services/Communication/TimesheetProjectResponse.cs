using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class TimesheetProjectResponse : BaseResponse
    {
        public TimesheetProject TimesheetProject{ get; private set; }

        private TimesheetProjectResponse(bool success, string message, TimesheetProject timesheetProject) : base(success, message)
        {
            TimesheetProject = timesheetProject;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TimesheetProjectResponse(TimesheetProject timesheetProject) : this(true, string.Empty, timesheetProject)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TimesheetProjectResponse(string message) : this(false, message, null)
        { }
    }
}
