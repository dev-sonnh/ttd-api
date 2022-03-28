using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class TimesheetTaskService : ITimesheetTaskService
    {
        private readonly ITimesheetTaskRepository _timesheetTaskRepository;
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly ITimesheetProjectRepository _timesheetProjectRepository;
        private readonly ITimesheetObjectRepository _timesheetObjectRepository;
        private readonly ITimesheetCategoryRepository _timesheetCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TimesheetTaskService(ITimesheetTaskRepository timesheetTaskRepository,
            ITimesheetCategoryRepository timesheetCategoryRepository,
            ITimesheetObjectRepository timesheetObjectRepository,
            ITimesheetProjectRepository timesheetProjectRepository,
            ITimesheetRepository timesheetRepository,
            IUnitOfWork unitOfWork)
        {
            _timesheetTaskRepository = timesheetTaskRepository;
            _timesheetRepository = timesheetRepository;
            _timesheetCategoryRepository = timesheetCategoryRepository;
            _timesheetObjectRepository = timesheetObjectRepository;
            _timesheetProjectRepository = timesheetProjectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimesheetTask>> GetAllTimesheetTasks()
        {
            return await _timesheetTaskRepository.GetAllTimesheetTasks();
        }

        public async Task<TimesheetTask> GetTimesheetTaskById(long id)
        {
            return await _timesheetTaskRepository.GetTimesheetTaskById(id);
        }

        public async Task<TimesheetTaskResponse> SaveTimesheetTask(TimesheetTask timesheetTask)
        {
            var existingTimesheet = await _timesheetRepository.GetTimesheetById(timesheetTask.TimesheetId);
            var existingProject = await _timesheetProjectRepository.GetTimesheetProjectById(timesheetTask.TimesheetProjectId);
            var existingObject = await _timesheetObjectRepository.GetTimesheetObjectById(timesheetTask.TimesheetObjectId);
            var existingCategory = await _timesheetCategoryRepository.GetTimesheetCategoryById(timesheetTask.TimesheetCategoryId);

            if (existingTimesheet == null)
                return new TimesheetTaskResponse($"There is no data for timesheet on this day, please contact to administrator!");

            if (existingProject == null)
                return new TimesheetTaskResponse($"This project does not exist on system, please contact to administrator!");

            if (existingObject == null)
                return new TimesheetTaskResponse($"This object does not exist on system, please contact to administrator!");

            if (existingCategory == null)
                return new TimesheetTaskResponse($"This category does not exist on system, please contact to administrator!");

            try
            {
                await _timesheetTaskRepository.CreateTimesheetTask(timesheetTask);

                return new TimesheetTaskResponse(timesheetTask);
            }
            catch (Exception ex)
            {
                return new TimesheetTaskResponse($"An error occurred when saving timesheetTask -.-! :{ex.Message}");
            }
        }

        public async Task<TimesheetTaskResponse> UpdateTimesheetTask(long id, TimesheetTask timesheetTask)
        {
            var existingTimesheetTask = await _timesheetTaskRepository.GetTimesheetTaskById(id);
            var existingTimesheet = await _timesheetRepository.GetTimesheetById(timesheetTask.TimesheetId);
            var existingProject = await _timesheetProjectRepository.GetTimesheetProjectById(timesheetTask.TimesheetProjectId);
            var existingObject = await _timesheetObjectRepository.GetTimesheetObjectById(timesheetTask.TimesheetObjectId);
            var existingCategory = await _timesheetCategoryRepository.GetTimesheetCategoryById(timesheetTask.TimesheetCategoryId);

            if (existingTimesheet == null)
                return new TimesheetTaskResponse($"There is no data for timesheet on this day, please contact to administrator!");

            if (existingProject == null)
                return new TimesheetTaskResponse($"This project does not exist on system, please contact to administrator!");

            if (existingObject == null)
                return new TimesheetTaskResponse($"This object does not exist on system, please contact to administrator!");

            if (existingCategory == null)
                return new TimesheetTaskResponse($"This category does not exist on system, please contact to administrator!");

            if (existingTimesheetTask == null)
                return new TimesheetTaskResponse("TimesheetTask is not found");

            existingTimesheetTask.TimesheetId = timesheetTask.TimesheetId;
            existingTimesheetTask.TimesheetProjectId = timesheetTask.TimesheetProjectId;
            existingTimesheetTask.TimesheetCategoryId = timesheetTask.TimesheetCategoryId;
            existingTimesheetTask.TimesheetObjectId = timesheetTask.TimesheetObjectId;
            existingTimesheetTask.Description = timesheetTask.Description;
            existingTimesheetTask.Hours = timesheetTask.Hours;
            existingTimesheetTask.IsOvertime = timesheetTask.IsOvertime;
            existingTimesheetTask.ModifiedBy = timesheetTask.ModifiedBy;
            existingTimesheetTask.ModifiedDate = timesheetTask.ModifiedDate;

            try
            {
                _timesheetTaskRepository.UpdateTimesheetTask(existingTimesheetTask);
                await _unitOfWork.CompleteAsync();

                return new TimesheetTaskResponse(existingTimesheetTask);
            }
            catch (Exception ex)
            {
                return new TimesheetTaskResponse($"An error occurred when updating timesheetTasks: {ex.Message}");
            }
        }

        public async Task<TimesheetTaskResponse> DeleteTimesheetTask(long id)
        {
            var existingTimesheetTask = await _timesheetTaskRepository.GetTimesheetTaskById(id);

            if (existingTimesheetTask == null)
                return new TimesheetTaskResponse("TimesheetTask is not found");
            try
            {
                _timesheetTaskRepository.DeleteTimesheetTask(existingTimesheetTask);
                await _unitOfWork.CompleteAsync();

                return new TimesheetTaskResponse(existingTimesheetTask);
            }
            catch (Exception ex)
            {
                return new TimesheetTaskResponse($"An error occurred when updating timesheetTasks: {ex.Message}");
            }
        }
    }
}
