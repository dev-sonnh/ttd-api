using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Services.Communication
{
    public class UserSettingResponse : BaseResponse
    {
        public UserSetting UserSetting{ get; private set; }

        private UserSettingResponse(bool success, string message, UserSetting userSetting) : base(success, message)
        {
            UserSetting = userSetting;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public UserSettingResponse(UserSetting userSetting) : this(true, string.Empty, userSetting)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public UserSettingResponse(string message) : this(false, message, null)
        { }
    }
}
