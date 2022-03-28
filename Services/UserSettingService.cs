using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class UserSettingService : IUserSettingService
    {
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserSettingService(IUserSettingRepository userSettingRepository, IUnitOfWork unitOfWork)
        {
            _userSettingRepository = userSettingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserSetting>> GetAllUserSettings()
        {
            return await _userSettingRepository.GetAllUserSettings();
        }

        public async Task<UserSetting> GetUserSettingById(long id)
        {
            return await _userSettingRepository.GetUserSettingById(id);
        }

        public async Task<UserSettingResponse> SaveUserSetting(UserSetting userSetting)
        {
            try
            {
                await _userSettingRepository.CreateUserSetting(userSetting);

                return new UserSettingResponse(userSetting);
            }
            catch (Exception ex)
            {
                return new UserSettingResponse($"An error occurred when saving userSetting -.-! :{ex.Message}");
            }
        }

        public async Task<UserSettingResponse> UpdateUserSetting(long id, UserSetting userSetting)
        {
            var existingUserSetting = await _userSettingRepository.GetUserSettingById(id);

            if (existingUserSetting == null)
                return new UserSettingResponse("UserSetting is not found");

            existingUserSetting.UserId=userSetting.UserId;
            existingUserSetting.EmailNotification=userSetting.EmailNotification;
            existingUserSetting.Timezone=userSetting.Timezone;
            existingUserSetting.Language = userSetting.Language;

            try
            {
                _userSettingRepository.UpdateUserSetting(existingUserSetting);
                await _unitOfWork.CompleteAsync();

                return new UserSettingResponse(existingUserSetting);
            }
            catch (Exception ex)
            {
                return new UserSettingResponse($"An error occurred when updating userSettings: {ex.Message}");
            }
        }

        public async Task<UserSettingResponse> DeleteUserSetting(long id)
        {
            var existingUserSetting = await _userSettingRepository.GetUserSettingById(id);

            if (existingUserSetting == null)
                return new UserSettingResponse("UserSetting is not found");
            try
            {
                _userSettingRepository.DeleteUserSetting(existingUserSetting);
                await _unitOfWork.CompleteAsync();

                return new UserSettingResponse(existingUserSetting);
            }
            catch (Exception ex)
            {
                return new UserSettingResponse($"An error occurred when updating userSettings: {ex.Message}");
            }
        }
    }
}
