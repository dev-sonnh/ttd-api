using Microsoft.EntityFrameworkCore;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class UserSettingRepository : BaseRepository, IUserSettingRepository
    {
        public UserSettingRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateUserSetting(UserSetting UserSetting)
        {
            await _context.UserSettings.AddAsync(UserSetting);
        }

        public void DeleteUserSetting(UserSetting UserSetting)
        {
            _context.UserSettings.Remove(UserSetting);
        }

        public async Task<IEnumerable<UserSetting>> GetAllUserSettings()
        {
            return await _context.UserSettings.ToListAsync();
        }

        public async Task<UserSetting> GetUserSettingById(long id)
        {
            return await _context.UserSettings.FindAsync(id);
        }

        public void UpdateUserSetting(UserSetting UserSetting)
        {
            _context.UserSettings.Update(UserSetting);
        }
    }
}
