using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class TeamUserService : ITeamUserService
    {
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamUserService(ITeamUserRepository teamUserRepository,
            IUnitOfWork unitOfWork)
        {
            _teamUserRepository = teamUserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TeamUser>> GetAllTeamUsers()
        {
            return await _teamUserRepository.GetAllTeamUsers();
        }

        public async Task<TeamUser> GetTeamUserById(long id)
        {
            return await _teamUserRepository.GetTeamUserById(id);
        }

        public async Task<TeamUserResponse> SaveTeamUser(TeamUser teamUser)
        {
            try
            {
                await _teamUserRepository.CreateTeamUser(teamUser);

                return new TeamUserResponse(teamUser);
            }
            catch (Exception ex)
            {
                return new TeamUserResponse($"An error occurred when saving teamUser -.-! :{ex.Message}");
            }
        }

        public async Task<TeamUserResponse> UpdateTeamUser(long id, TeamUser teamUser)
        {
            var existingTeamUser = await _teamUserRepository.GetTeamUserById(id);

            if (existingTeamUser == null)
                return new TeamUserResponse("TeamUser is not found");

            existingTeamUser.TeamId = teamUser.TeamId;
            existingTeamUser.UserId = teamUser.UserId;
            existingTeamUser.Status = teamUser.Status;
            existingTeamUser.ModifiedBy = teamUser.ModifiedBy;

            try
            {
                _teamUserRepository.UpdateTeamUser(existingTeamUser);
                await _unitOfWork.CompleteAsync();

                return new TeamUserResponse(existingTeamUser);
            }
            catch (Exception ex)
            {
                return new TeamUserResponse($"An error occurred when updating teamUsers: {ex.Message}");
            }
        }

        public async Task<TeamUserResponse> DeleteTeamUser(long id)
        {
            var existingTeamUser = await _teamUserRepository.GetTeamUserById(id);

            if (existingTeamUser == null)
                return new TeamUserResponse("TeamUser is not found");
            try
            {
                _teamUserRepository.DeleteTeamUser(existingTeamUser);
                await _unitOfWork.CompleteAsync();

                return new TeamUserResponse(existingTeamUser);
            }
            catch (Exception ex)
            {
                return new TeamUserResponse($"An error occurred when updating teamUsers: {ex.Message}");
            }
        }

        public async Task<TeamUserResponse> ChangeStatusById(long id, TeamUser teamUser)
        {
            var existingTeamUser = await _teamUserRepository.GetTeamUserByUserIdAndTeamId(id, teamUser.TeamId);

            if (existingTeamUser == null)
                return new TeamUserResponse("Data is not found");

            existingTeamUser.Status = teamUser.Status.ToLower();
            existingTeamUser.ModifiedBy = teamUser.ModifiedBy;

            try
            {
                _teamUserRepository.ChangeStatusById(id, existingTeamUser);
                await _unitOfWork.CompleteAsync();

                return new TeamUserResponse(existingTeamUser);
            }
            catch (Exception ex)
            {
                return new TeamUserResponse($"An error occurred when updating teamUsers: {ex.Message}");
            }
        }

        public async Task<TeamUser> GetTeamUserByUserIdAndTeamId(long id, long teamId)
        {
            return await _teamUserRepository.GetTeamUserByUserIdAndTeamId(id, teamId);
        }
    }
}
