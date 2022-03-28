using TTDesign.API.Domain.Models.Extended;

namespace TTDesign.API.Domain.Services.Communication.Extended
{
    public class UserCredentailResponse : BaseResponse
    {
        public VwUserCredential UserCredential { get; private set; }

        private UserCredentailResponse(bool success, string message, VwUserCredential userCredential) : base(success, message)
        {
            UserCredential = userCredential;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public UserCredentailResponse(VwUserCredential user) : this(true, string.Empty, user)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public UserCredentailResponse(string message) : this(false, message, null)
        { }
    }
}
