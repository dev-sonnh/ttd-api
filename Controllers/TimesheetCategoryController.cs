using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services;
using TTDesign.API.Extensions;
using TTDesign.API.Resources;

namespace TTDesign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetCategoryController : ControllerBase
    {
        private readonly ITimesheetCategoryService _timesheetCategoryService;
        private readonly IMapper _mapper;

        public TimesheetCategoryController(ITimesheetCategoryService timesheetCategoryService, IMapper mapper)
        {
            _timesheetCategoryService = timesheetCategoryService;
            _mapper = mapper;
        }


        // GET: api/TimesheetCategorys
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TimesheetCategoryResource>> GetAllTimesheetCategories()
        {
            var timesheetCategorys = await _timesheetCategoryService.GetAllTimesheetCategories();
            var resources = _mapper.Map<IEnumerable<TimesheetCategoryResource>>(timesheetCategorys);
            return resources;
        }

        [HttpGet("GetAllTimesheetCategoriesByTeamId/{id}")]
        [Authorize]
        public async Task<IEnumerable<TimesheetCategoryResource>> GetAllTimesheetCategoriesByTeamId(long id)
        {
            var timesheetCategorys = await _timesheetCategoryService.GetAllTimesheetCategoriesByTeamId(id);
            var resources = _mapper.Map<IEnumerable<TimesheetCategoryResource>>(timesheetCategorys);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<TimesheetCategoryResource> GetTimesheetCategoryById(long id)
        {
            var timesheetCategory = await _timesheetCategoryService.GetTimesheetCategoryById(id);
            var resource = _mapper.Map<TimesheetCategoryResource>(timesheetCategory);
            return resource;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateTimesheetCategory([FromBody] SaveTimesheetCategoryResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetCategory = _mapper.Map<SaveTimesheetCategoryResource, TimesheetCategory>(resource);
            var result = await _timesheetCategoryService.SaveTimesheetCategory(timesheetCategory);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetCategoryResource = _mapper.Map<TimesheetCategory, TimesheetCategoryResource>(result.TimesheetCategory);

            return Ok(timesheetCategoryResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTimesheetCategory(long id, [FromBody] SaveTimesheetCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetCategory = _mapper.Map<SaveTimesheetCategoryResource, TimesheetCategory>(resource);
            var result = await _timesheetCategoryService.UpdateTimesheetCategory(id, timesheetCategory);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetCategoryResource = _mapper.Map<TimesheetCategory, TimesheetCategoryResource>(result.TimesheetCategory);
            return Ok(timesheetCategoryResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTimesheetCategory(long id)
        {
            var result = await _timesheetCategoryService.DeleteTimesheetCategory(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetCategoryResource = _mapper.Map<TimesheetCategory, TimesheetCategoryResource>(result.TimesheetCategory);
            return Ok(timesheetCategoryResource);
        }
    }
}
