using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateUser(User user)
        {
            var parameters = FromModelToParams(user);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_Users_InsertUser({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12})", parameters);
        }

        public async Task CreateUserCredential(VwUserCredential credential, ApplicationRole[] userRoles)
        {
            var parameters = FromModelUserCredentialToParams(credential);

            await _context.Database.ExecuteSqlRawAsync("CALL usp_Users_InsertUserCredential({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12})", parameters);

            //add roles
            User curUser = (await _context.Users.FromSqlRaw("CALL usp_Users_GetUserByEmail({0})", parameters: credential.Email).ToListAsync()).FirstOrDefault();
            if (curUser != null)
            {
                var roleNames = userRoles.Select(r => r.ToString()).ToList();
                var roles = await _context.Roles.Where(r => roleNames.Contains(r.RoleName)).ToListAsync();

                foreach (var role in roles)
                {
                    curUser.UserRoles.Add(new UserRole { RoleId = role.RoleId });
                }

                _context.Users.Update(curUser);
            }
        }

        public async Task UpdateUserCredentialAsync(VwUserCredential userCredential)
        {
            var parameters = FromModelUserCredentialToUpdateParams(userCredential);

            await _context.Database.ExecuteSqlRawAsync("CALL usp_Users_UpdateUserCredential({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13})", parameters);
        }

        public async Task UpdateUserCredentialByCommonUserAsync(VwUserCredential userCredential)
        {
            var parameters = FromModelUserCredentialToUpdateCommonParams(userCredential);

            await _context.Database.ExecuteSqlRawAsync("CALL usp_Users_UpdateUserCredentialByCommonUser({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})", parameters);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.FromSqlRaw("CALL usp_Users_GetAllUsers").ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = (await _context.Users.FromSqlRaw("CALL usp_Users_GetUserByEmail({0})", parameters: email).ToListAsync()).FirstOrDefault();
            if(user != null)
            {
                var roles = await _context.UserRoles.Include(ur => ur.Role).Where(r => r.UserId == user.UserId).ToListAsync();

                user.UserRoles = roles;
            }
            return user;
        }

        public async Task<User> GetUserById(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<VwUserCredential> GetUserCredentialById(long id)
        {

            return (await _context.Set<VwUserCredential>().FromSqlRaw("CALL usp_Users_GetUserCredentialByUserId({0})", id).ToListAsync()).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        //parameters
        public List<MySqlParameter> FromModelUserCredentialToParams(VwUserCredential credential)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("full_name_parms", credential.FullName));
            parameters.Add(new MySqlParameter("password_parms", credential.Password));
            parameters.Add(new MySqlParameter("birthday_parms", credential.BirthDay));
            parameters.Add(new MySqlParameter("team_id_parms", credential.TeamId));
            parameters.Add(new MySqlParameter("role_parms", credential.Role));
            parameters.Add(new MySqlParameter("phone_number_parms", credential.PhoneNumber));
            parameters.Add(new MySqlParameter("email_parms", credential.Email));
            parameters.Add(new MySqlParameter("address_parms", credential.Address));
            parameters.Add(new MySqlParameter("avatar_parms", credential.Avatar));
            parameters.Add(new MySqlParameter("about_me_parms", credential.AboutMe));
            parameters.Add(new MySqlParameter("id_no_parms", credential.IdNo));
            parameters.Add(new MySqlParameter("issued_to_parms", credential.IssuedTo));
            parameters.Add(new MySqlParameter("created_by_parms", credential.CreatedBy));

            return parameters;
        }

        public List<MySqlParameter> FromModelUserCredentialToUpdateParams(VwUserCredential credential)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("user_id_parms", credential.UserId));
            parameters.Add(new MySqlParameter("full_name_parms", credential.FullName));
            parameters.Add(new MySqlParameter("password_parms", credential.Password));
            parameters.Add(new MySqlParameter("birthday_parms", credential.BirthDay));
            parameters.Add(new MySqlParameter("team_id_parms", credential.TeamId));
            parameters.Add(new MySqlParameter("role_parms", credential.Role));
            parameters.Add(new MySqlParameter("phone_number_parms", credential.PhoneNumber));
            parameters.Add(new MySqlParameter("email_parms", credential.Email));
            parameters.Add(new MySqlParameter("address_parms", credential.Address));
            parameters.Add(new MySqlParameter("avatar_parms", credential.Avatar));
            parameters.Add(new MySqlParameter("about_me_parms", credential.AboutMe));
            parameters.Add(new MySqlParameter("id_no_parms", credential.IdNo));
            parameters.Add(new MySqlParameter("issued_to_parms", credential.IssuedTo));
            parameters.Add(new MySqlParameter("created_by_parms", credential.CreatedBy));

            return parameters;
        }

        public List<MySqlParameter> FromModelUserCredentialToUpdateCommonParams(VwUserCredential credential)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("user_id_parms", credential.UserId));
            parameters.Add(new MySqlParameter("full_name_parms", credential.FullName));
            parameters.Add(new MySqlParameter("password_parms", credential.Password));
            parameters.Add(new MySqlParameter("birthday_parms", credential.BirthDay));
            parameters.Add(new MySqlParameter("phone_number_parms", credential.PhoneNumber));
            parameters.Add(new MySqlParameter("email_parms", credential.Email));
            parameters.Add(new MySqlParameter("address_parms", credential.Address));
            parameters.Add(new MySqlParameter("avatar_parms", credential.Avatar));
            parameters.Add(new MySqlParameter("about_me_parms", credential.AboutMe));
            parameters.Add(new MySqlParameter("id_no_parms", credential.IdNo));
            parameters.Add(new MySqlParameter("issued_to_parms", credential.IssuedTo));
            parameters.Add(new MySqlParameter("created_by_parms", credential.CreatedBy));

            return parameters;
        }

        public List<MySqlParameter> FromModelToParams(User user)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("user_name_parms", user.UserName));
            parameters.Add(new MySqlParameter("password_parms", user.Password));
            parameters.Add(new MySqlParameter("staff_id_parms", user.StaffId));
            parameters.Add(new MySqlParameter("full_name_parms", user.FullName));
            parameters.Add(new MySqlParameter("email_parms", user.Email));
            parameters.Add(new MySqlParameter("phone_number_parms", user.PhoneNumber));
            parameters.Add(new MySqlParameter("birthday_parms", user.Birthday));
            parameters.Add(new MySqlParameter("id_no_parms", user.IdNo));
            parameters.Add(new MySqlParameter("issued_to_parms", user.IssuedTo));
            parameters.Add(new MySqlParameter("address_parms", user.Address));
            parameters.Add(new MySqlParameter("about_me_parms", user.AboutMe));
            parameters.Add(new MySqlParameter("avatar_parms", user.Avatar));
            parameters.Add(new MySqlParameter("created_by_parms", user.CreatedBy));

            return parameters;
        }

        public async Task<User> GetUserByOrderNo(int orderNo)
        {
            return (await _context.Users.FromSqlRaw("CALL usp_Users_GetUserByOrderNo({0})", orderNo).ToListAsync()).FirstOrDefault();
        }

        public async Task<IEnumerable<VwUserCredential>> GetListUserCredentialByTeamId(long id)
        {
            return await _context.Set<VwUserCredential>().FromSqlRaw("CALL usp_Users_GetListUserCredentialByTeamId({0})", id).ToListAsync();
        }

        public async Task<VwUserCredential> GetUserCredentialByEmail(string email)
        {
            return (await _context.Set<VwUserCredential>().FromSqlRaw("CALL usp_Users_GetUserCredentialByEmail({0})", email).ToListAsync()).FirstOrDefault();
        }
    }
}
