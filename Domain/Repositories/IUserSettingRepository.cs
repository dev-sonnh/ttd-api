using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface IUserSettingRepository
    {
        Task<IEnumerable<UserSetting>> GetAllUserSettings();
        Task CreateUserSetting(UserSetting userSetting);
        Task<UserSetting> GetUserSettingById(long id);
        void UpdateUserSetting(UserSetting userSetting);
        void DeleteUserSetting(UserSetting userSetting);
    }
}
