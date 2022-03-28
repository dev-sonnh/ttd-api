using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(long id);
        Task<TeamResponse> SaveTeam(Team team);
        Task<TeamResponse> UpdateTeam(long id, Team team);
        Task<TeamResponse> DeleteTeam(long id);
    }
}
