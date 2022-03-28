using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.MySQL.Services
{
    public class TimesheetCategoryService : ITimesheetCategoryService
    {
        private readonly ITimesheetCategoryRepository _timesheetCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TimesheetCategoryService(ITimesheetCategoryRepository timesheetCategoryRepository, IUnitOfWork unitOfWork)
        {
            _timesheetCategoryRepository = timesheetCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategories()
        {
            return await _timesheetCategoryRepository.GetAllTimesheetCategories();
        }

        public async Task<TimesheetCategory> GetTimesheetCategoryById(long id)
        {
            return await _timesheetCategoryRepository.GetTimesheetCategoryById(id);
        }

        public async Task<TimesheetCategoryResponse> SaveTimesheetCategory(TimesheetCategory timesheetCategory)
        {
            try
            {
                await _timesheetCategoryRepository.CreateTimesheetCategory(timesheetCategory);

                return new TimesheetCategoryResponse(timesheetCategory);
            }
            catch (Exception ex)
            {
                return new TimesheetCategoryResponse($"An error occurred when saving TimesheetCategory -.-! :{ex.Message}");
            }
        }

        public async Task<TimesheetCategoryResponse> UpdateTimesheetCategory(long id, TimesheetCategory timesheetCategory)
        {
            var existingTimesheetCategory = await _timesheetCategoryRepository.GetTimesheetCategoryById(id);

            if (existingTimesheetCategory == null)
                return new TimesheetCategoryResponse("TimesheetCategory is not found");

            existingTimesheetCategory.Name = timesheetCategory.Name;
            existingTimesheetCategory.IsPublic = timesheetCategory.IsPublic;
            existingTimesheetCategory.IsActive = timesheetCategory.IsActive;
            existingTimesheetCategory.ModifiedBy = timesheetCategory.ModifiedBy;
            existingTimesheetCategory.ModifiedDate = timesheetCategory.ModifiedDate;

            try
            {
                _timesheetCategoryRepository.UpdateTimesheetCategory(existingTimesheetCategory);
                await _unitOfWork.CompleteAsync();

                return new TimesheetCategoryResponse(existingTimesheetCategory);
            }
            catch (Exception ex)
            {
                return new TimesheetCategoryResponse($"An error occurred when updating timesheetCategories: {ex.Message}");
            }
        }

        public async Task<TimesheetCategoryResponse> DeleteTimesheetCategory(long id)
        {
            var existingTimesheetCategory = await _timesheetCategoryRepository.GetTimesheetCategoryById(id);

            if (existingTimesheetCategory == null)
                return new TimesheetCategoryResponse("TimesheetCategory is not found");
            try
            {
                _timesheetCategoryRepository.DeleteTimesheetCategory(existingTimesheetCategory);
                await _unitOfWork.CompleteAsync();

                return new TimesheetCategoryResponse(existingTimesheetCategory);
            }
            catch (Exception ex)
            {
                return new TimesheetCategoryResponse($"An error occurred when updating timesheetCategorys: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategoriesByTeamId(long id)
        {
            return await _timesheetCategoryRepository.GetAllTimesheetCategoriesByTeamId(id);
        }
    }
}
