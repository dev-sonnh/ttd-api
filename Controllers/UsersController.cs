#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Services;
using AutoMapper;
using TTDesign.API.Resources;
using TTDesign.API.Extensions;
using TTDesign.API.Resources.Extended;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TTDesign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        // GET: api/Users
        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<IEnumerable<UserResource>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }

        [HttpGet("GetUserById/{id}")]
        [Authorize]
        public async Task<UserResource> GetUserById(long id)
        {
            var user = await _userService.GetUserById(id);
            var resource = _mapper.Map<UserResource>(user);
            return resource;
        }

        [HttpGet("GetUserByEmail/{email}")]
        [Authorize]
        public async Task<UserResource> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            var resource = _mapper.Map<UserResource>(user);
            return resource;
        }

        [HttpGet("GetUserByOrderNo/{orderNo}")]
        [Authorize]
        public async Task<UserResource> GetUserByOrderNo(int orderNo)
        {
            var user = await _userService.GetUserByOrderNo(orderNo);
            var resource = _mapper.Map<UserResource>(user);
            return resource;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateUser([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveUser(user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(result.User);

            return Ok(userResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.UpdateUser(id, user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteUser(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        //extented

        [HttpGet("GetUserCredentialById/{id}")]
        [Authorize]
        public async Task<ViewUserCredentialResource> GetUserCredentialById(long id)
        {
            var user = await _userService.GetUserCredentialById(id);
            var resource = _mapper.Map<ViewUserCredentialResource>(user);
            return resource;
        }

        [HttpGet("GetUserCredentialByEmail")]
        [Authorize]
        public async Task<ViewUserCredentialResource> GetUserCredentialByEmail(string email)
        {
            var user = await _userService.GetUserCredentialByEmail(email);
            var resource = _mapper.Map<ViewUserCredentialResource>(user);
            return resource;
        }

        [HttpGet("GetListUserCredentialByTeamId/{id}")]
        [Authorize]
        public async Task<IEnumerable<ViewUserCredentialResource>> GetListUserCredentialByTeamId(long id)
        {
            var user = await _userService.GetListUserCredentialByTeamId(id);
            var resource = _mapper.Map<IEnumerable<ViewUserCredentialResource>>(user);
            return resource;
        }

        [HttpPost("CreateNewUserCredential")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateNewUserCredential([FromBody] SaveUserCredentialResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = _mapper.Map<SaveUserCredentialResource, VwUserCredential>(resource);

            ApplicationRole applicationRole;

            if (resource.Role.ToLower() == "member")
            {
                applicationRole = ApplicationRole.Common;
            }
            else
            {
                applicationRole = ApplicationRole.Administrator;
            }

            var result = await _userService.CreateNewUserCredential(user, applicationRole);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<VwUserCredential, ViewUserCredentialResource>(result.UserCredential);

            return Ok(userResource);
        }

        [HttpPut("UpdateUserCredential")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateUserCredential([FromBody] SaveUserCredentialResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            resource.Role = resource.Role.ToLower();
            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = _mapper.Map<SaveUserCredentialResource, VwUserCredential>(resource);

            var result = await _userService.UpdateUserCredential(user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<VwUserCredential, ViewUserCredentialResource>(result.UserCredential);

            return Ok(userResource);
        }

        [HttpPut("UpdateUserCredentialByCommonUserAsync")]
        [Authorize]
        public async Task<IActionResult> UpdateUserCredentialByCommonUserAsync([FromBody] SaveUserCredentialResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            resource.Role = resource.Role.ToLower();
            resource.CreatedBy = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = _mapper.Map<SaveUserCredentialResource, VwUserCredential>(resource);

            var result = await _userService.UpdateUserCredentialByCommonUserAsync(user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var userResource = _mapper.Map<VwUserCredential, ViewUserCredentialResource>(result.UserCredential);

            return Ok(userResource);
        }
    }
}