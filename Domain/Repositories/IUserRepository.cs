using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;

namespace TTDesign.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task CreateUser(User user);
        Task<User> GetUserById(long id);
        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByOrderNo(int orderNo);

        void UpdateUser(User user);
        void DeleteUser(User user);

        //extended

        Task<VwUserCredential> GetUserCredentialById(long id);
        Task<VwUserCredential> GetUserCredentialByEmail(string email);

        Task<IEnumerable<VwUserCredential>> GetListUserCredentialByTeamId(long id);

        Task CreateUserCredential(VwUserCredential credential, ApplicationRole[] userRoles);

        Task UpdateUserCredentialAsync(VwUserCredential userCredential);

        Task UpdateUserCredentialByCommonUserAsync(VwUserCredential userCredential);
    }
}
