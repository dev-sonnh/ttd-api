using TTDesign.API.Domain.Models;

namespace TTDesign.API.Domain.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task CreateTeam(Team team);
        Task<Team> GetTeamById(long id);
        void UpdateTeam(Team team);
        void DeleteTeam(Team team);
    }
}
