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
    public class TimesheetTaskController : ControllerBase
    {
        private readonly ITimesheetTaskService _timesheetTaskService;
        private readonly IMapper _mapper;

        public TimesheetTaskController(ITimesheetTaskService timesheetTaskService, IMapper mapper)
        {
            _timesheetTaskService = timesheetTaskService;
            _mapper = mapper;
        }


        // GET: api/TimesheetTasks
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TimesheetTaskResource>> GetAllTimesheetTasks()
        {
            var timesheetTasks = await _timesheetTaskService.GetAllTimesheetTasks();
            var resources = _mapper.Map<IEnumerable<TimesheetTaskResource>>(timesheetTasks);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<TimesheetTaskResource> GetTimesheetTaskById(long id)
        {
            var timesheetTask = await _timesheetTaskService.GetTimesheetTaskById(id);
            var resource = _mapper.Map<TimesheetTaskResource>(timesheetTask);
            return resource;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTimesheetTask([FromBody] SaveTimesheetTaskResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetTask = _mapper.Map<SaveTimesheetTaskResource, TimesheetTask>(resource);
            var result = await _timesheetTaskService.SaveTimesheetTask(timesheetTask);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetTaskResource = _mapper.Map<TimesheetTask, TimesheetTaskResource>(result.TimesheetTask);

            return Ok(timesheetTaskResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTimesheetTask(long id, [FromBody] SaveTimesheetTaskResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetTask = _mapper.Map<SaveTimesheetTaskResource, TimesheetTask>(resource);
            var result = await _timesheetTaskService.UpdateTimesheetTask(id, timesheetTask);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetTaskResource = _mapper.Map<TimesheetTask, TimesheetTaskResource>(result.TimesheetTask);
            return Ok(timesheetTaskResource);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTimesheetTask(long id)
        {
            var result = await _timesheetTaskService.DeleteTimesheetTask(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetTaskResource = _mapper.Map<TimesheetTask, TimesheetTaskResource>(result.TimesheetTask);
            return Ok(timesheetTaskResource);
        }
    }
}
