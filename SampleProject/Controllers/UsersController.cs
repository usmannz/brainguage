using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API;
using SampleProject.Common;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Service.Contracts;

namespace SampleProject.Controllers
{
    [ApiController]
    [Authorize, Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [AllowAnonymous, HttpPost("auth")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginUser user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Something went wrong");
            }

            var response = await _userService.Authenticate(user.Email, user.Password);

            if (response == null)
            {

                return Unauthorized();
            }

            // else if (response.Tenant.StatusId != (int)TenantStatus.Active)
            // {

            //     return Ok(new { success = -1});
            // }

            else if (response.StatusId != (int)UserStatus.Active)
            {
                return Ok(new { success = -2});
            }

            return Ok(new { jwt = Jwt.Create(response), role = response.UserRoles, isEmail =response.isEmail, success = 1});
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task <ApiResponse<List<Users>>> Get()
        {
            return await _userService.GetAllUsers();

        }
    }
}
