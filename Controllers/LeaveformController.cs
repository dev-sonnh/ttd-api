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
    public class LeaveformController : ControllerBase
    {
        private readonly ILeaveformService _leaveformService;
        private readonly IMapper _mapper;

        public LeaveformController(ILeaveformService leaveformService, IMapper mapper)
        {
            _leaveformService = leaveformService;
            _mapper = mapper;
        }


        // GET: api/Leaveforms
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<LeaveformResource>> GetAllLeaveforms()
        {
            var leaveforms = await _leaveformService.GetAllLeaveforms();
            var resources = _mapper.Map<IEnumerable<LeaveformResource>>(leaveforms);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<LeaveformResource> GetLeaveformById(long id)
        {
            var leaveform = await _leaveformService.GetLeaveformById(id);
            var resource = _mapper.Map<LeaveformResource>(leaveform);
            return resource;
        }

        [HttpGet("GetLeaveformByUserRole/{id}")]
        [Authorize]
        public async Task<IEnumerable<LeaveformResource>> GetLeaveformByUserRole(long id)
        {
            var leaveform = await _leaveformService.GetLeaveformByUserRole(id);
            var resource = _mapper.Map<IEnumerable<LeaveformResource>>(leaveform);
            return resource;
        }

        [HttpGet("GetLeaveformByUserIdAndCurrentYear/{id}")]
        [Authorize]
        public async Task<IEnumerable<LeaveformResource>> GetLeaveformByUserIdAndCurrentYear(long id)
        {
            var leaveform = await _leaveformService.GetLeaveformByUserIdAndCurrentYear(id);
            var resource = _mapper.Map<IEnumerable<LeaveformResource>>(leaveform);
            return resource;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateLeaveform([FromBody] SaveLeaveformResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var leaveform = _mapper.Map<SaveLeaveformResource, Leaveform>(resource);
            var result = await _leaveformService.SaveLeaveform(leaveform);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var leaveformResource = _mapper.Map<Leaveform, LeaveformResource>(result.Leaveform);

            return Ok(leaveformResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLeaveform(long id, [FromBody] SaveLeaveformResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var leaveform = _mapper.Map<SaveLeaveformResource, Leaveform>(resource);
            var result = await _leaveformService.UpdateLeaveform(id, leaveform);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var leaveformResource = _mapper.Map<Leaveform, LeaveformResource>(result.Leaveform);
            return Ok(leaveformResource);
        }

        [HttpPut("ChangeStatusOfRequest/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ChangeStatusOfRequest(long id, [FromBody] SaveLeaveformResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var leaveform = _mapper.Map<SaveLeaveformResource, Leaveform>(resource);
            var result = await _leaveformService.ChangeStatusOfRequest(id, leaveform);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var leaveformResource = _mapper.Map<Leaveform, LeaveformResource>(result.Leaveform);
            return Ok(leaveformResource);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLeaveform(long id)
        {
            var result = await _leaveformService.DeleteLeaveform(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var leaveformResource = _mapper.Map<Leaveform, LeaveformResource>(result.Leaveform);
            return Ok(leaveformResource);
        }
    }
}
