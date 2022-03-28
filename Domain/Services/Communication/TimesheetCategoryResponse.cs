using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class TimesheetCategoryResponse : BaseResponse
    {
        public TimesheetCategory TimesheetCategory{ get; private set; }

        private TimesheetCategoryResponse(bool success, string message, TimesheetCategory timesheetCategory) : base(success, message)
        {
            TimesheetCategory = timesheetCategory;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TimesheetCategoryResponse(TimesheetCategory timesheetCategory) : this(true, string.Empty, timesheetCategory)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TimesheetCategoryResponse(string message) : this(false, message, null)
        { }
    }
}
