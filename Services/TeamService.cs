using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _teamRepository.GetAllTeams();
        }

        public async Task<Team> GetTeamById(long id)
        {
            return await _teamRepository.GetTeamById(id);
        }

        public async Task<TeamResponse> SaveTeam(Team team)
        {
            try
            {
                await _teamRepository.CreateTeam(team);

                return new TeamResponse(team);
            }
            catch (Exception ex)
            {
                return new TeamResponse($"An error occurred when saving team -.-! :{ex.Message}");
            }
        }

        public async Task<TeamResponse> UpdateTeam(long id, Team team)
        {
            var existingTeam = await _teamRepository.GetTeamById(id);

            if (existingTeam == null)
                return new TeamResponse("Team is not found");

            existingTeam.TeamName = team.TeamName;
            existingTeam.TeamDescription = team.TeamDescription;
            existingTeam.TeamCode = team.TeamCode;
            existingTeam.IsDepartment = team.IsDepartment;

            try
            {
                _teamRepository.UpdateTeam(existingTeam);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(existingTeam);
            }
            catch (Exception ex)
            {
                return new TeamResponse($"An error occurred when updating teams: {ex.Message}");
            }
        }

        public async Task<TeamResponse> DeleteTeam(long id)
        {
            var existingTeam = await _teamRepository.GetTeamById(id);

            if (existingTeam == null)
                return new TeamResponse("Team is not found");
            try
            {
                _teamRepository.DeleteTeam(existingTeam);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(existingTeam);
            }
            catch (Exception ex)
            {
                return new TeamResponse($"An error occurred when updating teams: {ex.Message}");
            }
        }
    }
}
