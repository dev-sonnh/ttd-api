using System.IdentityModel.Tokens.Jwt;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Security.Hashing;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;
using TTDesign.API.Domain.Services.Communication.Extended;

namespace TTDesign.API.MySQL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(long id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<UserResponse> SaveUser(User user)
        {
            try
            {
                await _userRepository.CreateUser(user);

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when saving user -.-! :{ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateUser(long id, User user)
        {
            var existingUser = await _userRepository.GetUserById(id);

            if (existingUser == null)
                return new UserResponse("User is not found");

            existingUser.Avatar = user.Avatar;
            existingUser.Birthday = user.Birthday;
            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.FullName = user.FullName;
            existingUser.StaffId = user.StaffId;

            try
            {
                _userRepository.UpdateUser(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when updating users: {ex.Message}");
            }
        }

        public async Task<UserResponse> DeleteUser(long id)
        {
            var existingUser = await _userRepository.GetUserById(id);

            if (existingUser == null)
                return new UserResponse("User is not found");
            try
            {
                _userRepository.DeleteUser(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error occurred when updating users: {ex.Message}");
            }
        }

        public async Task<UserCredentailResponse> CreateNewUserCredential(VwUserCredential user, params ApplicationRole[] userRoles)
        {
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if(existingUser != null)
            {
                return new UserCredentailResponse("Email already in use.");
            }

            user.Password = _passwordHasher.HashPassword("ttdesignco"); //default password for new users
           
            if(user.CreatedBy == null)
            {
                user.CreatedBy = 1;
            }

            try
            {
                await _userRepository.CreateUserCredential(user, userRoles);
                await _unitOfWork.CompleteAsync();

                return new UserCredentailResponse(user);
            }
            catch(Exception ex)
            {
                return new UserCredentailResponse($"An error occurred when create new user: {ex.Message}");
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<VwUserCredential> GetUserCredentialById(long id)
        {
            return await _userRepository.GetUserCredentialById(id);
        }

        public async Task<User> GetUserByOrderNo(int orderNo)
        {
            return await _userRepository.GetUserByOrderNo(orderNo);
        }

        public async Task<IEnumerable<VwUserCredential>> GetListUserCredentialByTeamId(long id)
        {
            return await _userRepository.GetListUserCredentialByTeamId(id);
        }

        public async Task<UserCredentailResponse> UpdateUserCredential(VwUserCredential user)
        {
            var existingUser = await _userRepository.GetUserCredentialById(user.UserId);
            if (existingUser == null)
            {
                return new UserCredentailResponse("User not found");
            }

            try
            {
                await _userRepository.UpdateUserCredentialAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserCredentailResponse(user);
            }
            catch (Exception ex)
            {
                return new UserCredentailResponse($"An error occurred when update new user: {ex.Message}");
            }
        }

        public async Task<UserCredentailResponse> UpdateUserCredentialByCommonUserAsync(VwUserCredential user)
        {
            var existingUser = await _userRepository.GetUserCredentialById(user.UserId);
            if (existingUser == null)
            {
                return new UserCredentailResponse("User not found");
            }

            try
            {
                await _userRepository.UpdateUserCredentialByCommonUserAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserCredentailResponse(user);
            }
            catch (Exception ex)
            {
                return new UserCredentailResponse($"An error occurred when update new user: {ex.Message}");
            }
        }

        public async Task<VwUserCredential> GetUserCredentialByEmail(string email)
        {
            return await _userRepository.GetUserCredentialByEmail(email);
        }
    }
}
