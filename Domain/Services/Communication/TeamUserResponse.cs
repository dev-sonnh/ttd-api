using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class TeamUserResponse : BaseResponse
    {
        public TeamUser TeamUser{ get; private set; }

        private TeamUserResponse(bool success, string message, TeamUser teamUser) : base(success, message)
        {
            TeamUser = teamUser;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public TeamUserResponse(TeamUser teamUser) : this(true, string.Empty, teamUser)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TeamUserResponse(string message) : this(false, message, null)
        { }
    }
}
