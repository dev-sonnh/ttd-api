using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Services;
using TTDesign.API.Extensions;
using TTDesign.API.Resources;

namespace TTDesign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingController : ControllerBase
    {
        private readonly IUserSettingService _userSettingService;
        private readonly IMapper _mapper;

        public UserSettingController(IUserSettingService userSettingService, IMapper mapper)
        {
            _userSettingService = userSettingService;
            _mapper = mapper;
        }


        // GET: api/UserSettings
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserSettingResource>> GetAllUserSettings()
        {
            var userSettings = await _userSettingService.GetAllUserSettings();
            var resources = _mapper.Map<IEnumerable<UserSettingResource>>(userSettings);
            return resources;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<UserSettingResource> GetUserSettingById(long id)
        {
            var userSetting = await _userSettingService.GetUserSettingById(id);
            var resource = _mapper.Map<UserSettingResource>(userSetting);
            return resource;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserSetting([FromBody] SaveUserSettingResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var userSetting = _mapper.Map<SaveUserSettingResource, UserSetting>(resource);
            var result = await _userSettingService.SaveUserSetting(userSetting);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userSettingResource = _mapper.Map<UserSetting, UserSettingResource>(result.UserSetting);

            return Ok(userSettingResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserSetting(long id, [FromBody] SaveUserSettingResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userSetting = _mapper.Map<SaveUserSettingResource, UserSetting>(resource);
            var result = await _userSettingService.UpdateUserSetting(id, userSetting);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userSettingResource = _mapper.Map<UserSetting, UserSettingResource>(result.UserSetting);
            return Ok(userSettingResource);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserSetting(long id)
        {
            var result = await _userSettingService.DeleteUserSetting(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userSettingResource = _mapper.Map<UserSetting, UserSettingResource>(result.UserSetting);
            return Ok(userSettingResource);
        }
    }
}
