using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services;
using TTDesign.API.Extensions;
using TTDesign.API.Resources;
using TTDesign.API.Resources.Extended;

namespace TTDesign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;
        private readonly IMapper _mapper;

        public TimesheetController(ITimesheetService timesheetService, IMapper mapper)
        {
            _timesheetService = timesheetService;
            _mapper = mapper;
        }


        // GET: api/Timesheets
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TimesheetResource>> GetAllTimesheets()
        {
            var timesheets = await _timesheetService.GetAllTimesheets();
            var resources = _mapper.Map<IEnumerable<TimesheetResource>>(timesheets);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<TimesheetResource> GetTimesheetById(long id)
        {
            var timesheet = await _timesheetService.GetTimesheetById(id);
            var resource = _mapper.Map<TimesheetResource>(timesheet);
            return resource;
        }

        [HttpGet("GetValidTimesheetByOrderNoAndDate/{orderNo}/{date}")]
        [Authorize]
        public async Task<TimesheetResource> GetValidTimesheetByOrderNoAndDate(int orderNo, DateTime date)
        {
            var timesheet = await _timesheetService.GetValidTimesheetbyOrderNoAndDate(orderNo, date);
            var resource = _mapper.Map<TimesheetResource>(timesheet);
            return resource;
        }

        [HttpPost]
        [Authorize()]
        public async Task<IActionResult> CreateTimesheet([FromBody] SaveTimesheetResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheet = _mapper.Map<SaveTimesheetResource, Timesheet>(resource);
            var result = await _timesheetService.SaveTimesheet(timesheet);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetResource = _mapper.Map<Timesheet, TimesheetResource>(result.Timesheet);

            return Ok(timesheetResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTimesheet(long id, [FromBody] SaveTimesheetResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var timesheet = _mapper.Map<SaveTimesheetResource, Timesheet>(resource);
            var result = await _timesheetService.UpdateTimesheet(id, timesheet);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetResource = _mapper.Map<Timesheet, TimesheetResource>(result.Timesheet);
            return Ok(timesheetResource);
        }

        [HttpPut("UpdateTimesheetByFingerprintMachine")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTimesheetByFingerprintMachine([FromBody] UpdateTimesheetByFingerprintMachineResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var timesheet = _mapper.Map<UpdateTimesheetByFingerprintMachineResource, Timesheet>(resource);
            var result = await _timesheetService.UpdateTimesheetByFingerprintMachine(timesheet);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetResource = _mapper.Map<Timesheet, TimesheetResource>(result.Timesheet);
            return Ok(timesheetResource);
        }

        [HttpPut("UpdateTimesheetByFingerprintMachineMultiple")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTimesheetByFingerprintMachineMultiple([FromBody] IEnumerable<UpdateTimesheetByFingerprintMachineResource> resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var timesheet = _mapper.Map<IEnumerable<UpdateTimesheetByFingerprintMachineResource>, IEnumerable<Timesheet>>(resource);
            var result = await _timesheetService.UpdateTimesheetByFingerprintMachineMultiple(timesheet);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            //var timesheetResource = _mapper.Map<IEnumerable<Timesheet>, IEnumerable<TimesheetResource>>(result.Timesheet);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTimesheet(long id)
        {
            var result = await _timesheetService.DeleteTimesheet(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var timesheetResource = _mapper.Map<Timesheet, TimesheetResource>(result.Timesheet);
            return Ok(timesheetResource);
        }
    }
}
