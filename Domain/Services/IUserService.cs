using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Services.Communication;
using TTDesign.API.Domain.Services.Communication.Extended;

namespace TTDesign.API.Domain.Services
{
    public interface IUserService
    {
        //common services
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(long id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByOrderNo(int orderNo);
        Task<UserResponse> SaveUser(User user);
        Task<UserResponse> UpdateUser(long id, User user);
        Task<UserResponse> DeleteUser(long id);


        //extended services
        Task<VwUserCredential> GetUserCredentialById(long id);

        Task<VwUserCredential> GetUserCredentialByEmail(string email);

        Task<IEnumerable<VwUserCredential>> GetListUserCredentialByTeamId(long id);

        Task<UserCredentailResponse> CreateNewUserCredential(VwUserCredential user, params ApplicationRole[] userRoles);

        Task<UserCredentailResponse> UpdateUserCredential(VwUserCredential user);

        Task<UserCredentailResponse> UpdateUserCredentialByCommonUserAsync(VwUserCredential user);
    }
}
