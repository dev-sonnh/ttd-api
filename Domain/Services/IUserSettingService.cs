using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface IUserSettingService
    {
        Task<IEnumerable<UserSetting>> GetAllUserSettings();
        Task<UserSetting> GetUserSettingById(long id);
        Task<UserSettingResponse> SaveUserSetting(UserSetting userSetting);
        Task<UserSettingResponse> UpdateUserSetting(long id, UserSetting userSetting);
        Task<UserSettingResponse> DeleteUserSetting(long id);
    }
}
