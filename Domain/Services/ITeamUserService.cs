using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ITeamUserService
    {
        Task<IEnumerable<TeamUser>> GetAllTeamUsers();
        Task<TeamUser> GetTeamUserById(long id);
        Task<TeamUserResponse> SaveTeamUser(TeamUser teamUser);
        Task<TeamUserResponse> UpdateTeamUser(long id, TeamUser teamUser);
        Task<TeamUserResponse> ChangeStatusById(long id, TeamUser teamUse);
        Task<TeamUser> GetTeamUserByUserIdAndTeamId(long id, long teamId);
        Task<TeamUserResponse> DeleteTeamUser(long id);
    }
}
