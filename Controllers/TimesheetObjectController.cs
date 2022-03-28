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
    public class TimesheetObjectController : ControllerBase
    {
        private readonly ITimesheetObjectService _timesheetObjectService;
        private readonly IMapper _mapper;

        public TimesheetObjectController(ITimesheetObjectService timesheetObjectService, IMapper mapper)
        {
            _timesheetObjectService = timesheetObjectService;
            _mapper = mapper;
        }


        // GET: api/TimesheetObjects
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TimesheetObjectResource>> GetAllTimesheetObjects()
        {
            var timesheetObjects = await _timesheetObjectService.GetAllTimesheetObjects();
            var resources = _mapper.Map<IEnumerable<TimesheetObjectResource>>(timesheetObjects);
            return resources;
        }
        
        [HttpGet("GetAllTimesheetObjectsByTeamId/{id}")]
        [Authorize]
        public async Task<IEnumerable<TimesheetObjectResource>> GetAllTimesheetObjectsByTeamId(long id)
        {
            var timesheetObjects = await _timesheetObjectService.GetAllTimesheetObjectsByTeamId(id);
            var resources = _mapper.Map<IEnumerable<TimesheetObjectResource>>(timesheetObjects);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<TimesheetObjectResource> GetTimesheetObjectById(long id)
        {
            var timesheetObject = await _timesheetObjectService.GetTimesheetObjectById(id);
            var resource = _mapper.Map<TimesheetObjectResource>(timesheetObject);
            return resource;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateTimesheetObject([FromBody] SaveTimesheetObjectResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetObject = _mapper.Map<SaveTimesheetObjectResource, TimesheetObject>(resource);
            var result = await _timesheetObjectService.SaveTimesheetObject(timesheetObject);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetObjectResource = _mapper.Map<TimesheetObject, TimesheetObjectResource>(result.TimesheetObject);

            return Ok(timesheetObjectResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTimesheetObject(long id, [FromBody] SaveTimesheetObjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheetObject = _mapper.Map<SaveTimesheetObjectResource, TimesheetObject>(resource);
            var result = await _timesheetObjectService.UpdateTimesheetObject(id, timesheetObject);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetObjectResource = _mapper.Map<TimesheetObject, TimesheetObjectResource>(result.TimesheetObject);
            return Ok(timesheetObjectResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTimesheetObject(long id)
        {
            var result = await _timesheetObjectService.DeleteTimesheetObject(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetObjectResource = _mapper.Map<TimesheetObject, TimesheetObjectResource>(result.TimesheetObject);
            return Ok(timesheetObjectResource);
        }
    }
}
