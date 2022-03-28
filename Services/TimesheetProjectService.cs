using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class TimesheetProjectService : ITimesheetProjectService
    {
        private readonly ITimesheetProjectRepository _timesheetProjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TimesheetProjectService(ITimesheetProjectRepository timesheetProjectRepository, IUnitOfWork unitOfWork)
        {
            _timesheetProjectRepository = timesheetProjectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjects()
        {
            return await _timesheetProjectRepository.GetAllTimesheetProjects();
        }

        public async Task<TimesheetProject> GetTimesheetProjectById(long id)
        {
            return await _timesheetProjectRepository.GetTimesheetProjectById(id);
        }

        public async Task<TimesheetProjectResponse> SaveTimesheetProject(TimesheetProject timesheetProject)
        {
            try
            {
                await _timesheetProjectRepository.CreateTimesheetProject(timesheetProject);
                await _unitOfWork.CompleteAsync();

                return new TimesheetProjectResponse(timesheetProject);
            }
            catch (Exception ex)
            {
                return new TimesheetProjectResponse($"An error occurred when saving timesheetProject -.-! :{ex.Message}");
            }
        }

        public async Task<TimesheetProjectResponse> UpdateTimesheetProject(long id, TimesheetProject timesheetProject)
        {
            var existingTimesheetProject = await _timesheetProjectRepository.GetTimesheetProjectById(id);

            if (existingTimesheetProject == null)
                return new TimesheetProjectResponse("TimesheetProject is not found");

            existingTimesheetProject.Code = timesheetProject.Code;
            existingTimesheetProject.Name = timesheetProject.Name;
            existingTimesheetProject.StartedDate = timesheetProject.StartedDate;
            existingTimesheetProject.FinishedDate = timesheetProject.FinishedDate;
            existingTimesheetProject.IsActive = timesheetProject.IsActive;
            existingTimesheetProject.IsPublic = timesheetProject.IsPublic;
            existingTimesheetProject.ModifiedBy = timesheetProject.ModifiedBy;
            existingTimesheetProject.ModifiedDate = timesheetProject.ModifiedDate;

            try
            {
                _timesheetProjectRepository.UpdateTimesheetProject(existingTimesheetProject);
                await _unitOfWork.CompleteAsync();

                return new TimesheetProjectResponse(existingTimesheetProject);
            }
            catch (Exception ex)
            {
                return new TimesheetProjectResponse($"An error occurred when updating timesheetProjects: {ex.Message}");
            }
        }

        public async Task<TimesheetProjectResponse> DeleteTimesheetProject(long id)
        {
            var existingTimesheetProject = await _timesheetProjectRepository.GetTimesheetProjectById(id);

            if (existingTimesheetProject == null)
                return new TimesheetProjectResponse("TimesheetProject is not found");
            try
            {
                _timesheetProjectRepository.DeleteTimesheetProject(existingTimesheetProject);
                await _unitOfWork.CompleteAsync();

                return new TimesheetProjectResponse(existingTimesheetProject);
            }
            catch (Exception ex)
            {
                return new TimesheetProjectResponse($"An error occurred when updating timesheetProjects: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjectsByTeamId(long id)
        {
            return await _timesheetProjectRepository.GetAllTimesheetProjectsByTeamId(id);
        }
    }
}
