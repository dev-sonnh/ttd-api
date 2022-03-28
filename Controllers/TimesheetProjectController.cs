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
    public class TimesheetProjectController : ControllerBase
    {
        private readonly ITimesheetProjectService _timesheetProjectService;
        private readonly IMapper _mapper;

        public TimesheetProjectController(ITimesheetProjectService timesheetProjectService, IMapper mapper)
        {
            _timesheetProjectService = timesheetProjectService;
            _mapper = mapper;
        }


        // GET: api/TimesheetProjects
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TimesheetProjectResource>> GetAllTimesheetProjects()
        {
            var timesheetProjects = await _timesheetProjectService.GetAllTimesheetProjects();
            var resources = _mapper.Map<IEnumerable<TimesheetProjectResource>>(timesheetProjects);
            return resources;
        }

        [HttpGet("GetAllTimesheetProjectsByTeamId/{id}")]
        [Authorize]
        public async Task<IEnumerable<TimesheetProjectResource>> GetAllTimesheetProjectsByTeamId(long id)
        {
            var timesheetProjects = await _timesheetProjectService.GetAllTimesheetProjectsByTeamId(id);
            var resources = _mapper.Map<IEnumerable<TimesheetProjectResource>>(timesheetProjects);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<TimesheetProjectResource> GetTimesheetProjectById(long id)
        {
            var timesheetProject = await _timesheetProjectService.GetTimesheetProjectById(id);
            var resource = _mapper.Map<TimesheetProjectResource>(timesheetProject);
            return resource;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateTimesheetProject([FromBody] SaveTimesheetProjectResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetProject = _mapper.Map<SaveTimesheetProjectResource, TimesheetProject>(resource);
            var result = await _timesheetProjectService.SaveTimesheetProject(timesheetProject);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetProjectResource = _mapper.Map<TimesheetProject, TimesheetProjectResource>(result.TimesheetProject);

            return Ok(timesheetProjectResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTimesheetProject(long id, [FromBody] SaveTimesheetProjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetProject = _mapper.Map<SaveTimesheetProjectResource, TimesheetProject>(resource);
            var result = await _timesheetProjectService.UpdateTimesheetProject(id, timesheetProject);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetProjectResource = _mapper.Map<TimesheetProject, TimesheetProjectResource>(result.TimesheetProject);
            return Ok(timesheetProjectResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTimesheetProject(long id)
        {
            var result = await _timesheetProjectService.DeleteTimesheetProject(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetProjectResource = _mapper.Map<TimesheetProject, TimesheetProjectResource>(result.TimesheetProject);
            return Ok(timesheetProjectResource);
        }
    }
}
