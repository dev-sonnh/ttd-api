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
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }


        // GET: api/Teams
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<TeamResource>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeams();
            var resources = _mapper.Map<IEnumerable<TeamResource>>(teams);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<TeamResource> GetTeamById(long id)
        {
            var team = await _teamService.GetTeamById(id);
            var resource = _mapper.Map<TeamResource>(team);
            return resource;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateTeam([FromBody] SaveTeamResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var team = _mapper.Map<SaveTeamResource, Team>(resource);
            var result = await _teamService.SaveTeam(team);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamResource = _mapper.Map<Team, TeamResource>(result.Team);

            return Ok(teamResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTeam(long id, [FromBody] SaveTeamResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.ModifiedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var team = _mapper.Map<SaveTeamResource, Team>(resource);
            var result = await _teamService.UpdateTeam(id, team);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamResource = _mapper.Map<Team, TeamResource>(result.Team);
            return Ok(teamResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTeam(long id)
        {
            var result = await _teamService.DeleteTeam(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var teamResource = _mapper.Map<Team, TeamResource>(result.Team);
            return Ok(teamResource);
        }
    }
}
