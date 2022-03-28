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
    public class TeamUserController : ControllerBase
    {
        private readonly ITeamUserService _teamUserService;
        private readonly IMapper _mapper;

        public TeamUserController(ITeamUserService teamUserService, IMapper mapper)
        {
            _teamUserService = teamUserService;
            _mapper = mapper;
        }


        // GET: api/TeamUsers
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TeamUserResource>> GetAllTeamUsers()
        {
            var teamUsers = await _teamUserService.GetAllTeamUsers();
            var resources = _mapper.Map<IEnumerable<TeamUserResource>>(teamUsers);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<TeamUserResource> GetTeamUserById(long id)
        {
            var teamUser = await _teamUserService.GetTeamUserById(id);
            var resource = _mapper.Map<TeamUserResource>(teamUser);
            return resource;
        }

        [HttpGet("GetTeamUserByUserIdAndTeamId/{id}/{teamId}")]
        [Authorize]
        public async Task<TeamUserResource> GetTeamUserByUserIdAndTeamId(long id, long teamId)
        {
            var teamUser = await _teamUserService.GetTeamUserByUserIdAndTeamId(id, teamId);
            var resource = _mapper.Map<TeamUserResource>(teamUser);
            return resource;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTeamUser([FromBody] SaveTeamUserResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var teamUser = _mapper.Map<SaveTeamUserResource, TeamUser>(resource);
            var result = await _teamUserService.SaveTeamUser(teamUser);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamUserResource = _mapper.Map<TeamUser, TeamUserResource>(result.TeamUser);

            return Ok(teamUserResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTeamUser(long id, [FromBody] SaveTeamUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var teamUser = _mapper.Map<SaveTeamUserResource, TeamUser>(resource);
            var result = await _teamUserService.UpdateTeamUser(id, teamUser);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamUserResource = _mapper.Map<TeamUser, TeamUserResource>(result.TeamUser);
            return Ok(teamUserResource);
        }

        [HttpPut("ChangeStatusById/{userId}")]
        [Authorize]
        public async Task<IActionResult> ChangeStatusById(long userId, [FromBody] SaveTeamUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var teamUser = _mapper.Map<SaveTeamUserResource, TeamUser>(resource);

            var result = await _teamUserService.ChangeStatusById(userId, teamUser);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamUserResource = _mapper.Map<TeamUser, TeamUserResource>(result.TeamUser);
            return Ok(teamUserResource);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTeamUser(long id)
        {
            var result = await _teamUserService.DeleteTeamUser(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamUserResource = _mapper.Map<TeamUser, TeamUserResource>(result.TeamUser);
            return Ok(teamUserResource);
        }
    }
}
