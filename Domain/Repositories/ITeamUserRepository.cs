using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ITeamUserRepository
    {
        Task<IEnumerable<TeamUser>> GetAllTeamUsers();
        Task CreateTeamUser(TeamUser teamUser);
        Task<TeamUser> GetTeamUserById(long id);
        Task<TeamUser> GetTeamUserByUserIdAndTeamId(long userId, long teamId);

        void UpdateTeamUser(TeamUser teamUser);
        void ChangeStatusById(long id, TeamUser teamUser);
        void DeleteTeamUser(TeamUser teamUser);
    }
}
