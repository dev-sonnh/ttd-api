using TTDesign.API.Domain.Security.Tokens;

namespace TTDesign.API.Domain.Services.Communication
{
    public class TokenResponse : BaseResponse
    {
        public AccessToken Token { get; set; }

        public TokenResponse(bool success, string message, AccessToken token): base(success, message)
        {
            Token = token;
        }
    }
}
