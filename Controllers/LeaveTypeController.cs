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
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public LeaveTypeController(ILeaveTypeService leaveTypeService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
        }


        // GET: api/LeaveTypes
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<LeaveTypeResource>> GetAllLeaveTypes()
        {
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypes();
            var resources = _mapper.Map<IEnumerable<LeaveTypeResource>>(leaveTypes);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<LeaveTypeResource> GetLeaveTypeById(long id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeById(id);
            var resource = _mapper.Map<LeaveTypeResource>(leaveType);
            return resource;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateLeaveType([FromBody] SaveLeaveTypeResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var leaveType = _mapper.Map<SaveLeaveTypeResource, LeaveType>(resource);
            var result = await _leaveTypeService.SaveLeaveType(leaveType);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var leaveTypeResource = _mapper.Map<LeaveType, LeaveTypeResource>(result.LeaveType);

            return Ok(leaveTypeResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLeaveType(long id, [FromBody] SaveLeaveTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var leaveType = _mapper.Map<SaveLeaveTypeResource, LeaveType>(resource);
            var result = await _leaveTypeService.UpdateLeaveType(id, leaveType);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var leaveTypeResource = _mapper.Map<LeaveType, LeaveTypeResource>(result.LeaveType);
            return Ok(leaveTypeResource);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLeaveType(long id)
        {
            var result = await _leaveTypeService.DeleteLeaveType(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var leaveTypeResource = _mapper.Map<LeaveType, LeaveTypeResource>(result.LeaveType);
            return Ok(leaveTypeResource);
        }
    }
}
