using Domain.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSIMSWebApi.Authentication;
using POSIMSWebApi.Authentication.Dtos;
using POSIMSWebApi.Authentication.Interface;


namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthServices _userAuthServices;
        public UserAuthController(IUserAuthServices userAuthServices)
        {
            _userAuthServices = userAuthServices;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<UserLoginDto>>> Login([FromQuery] LoginUserDto login)
        {
            var result = await _userAuthServices.LoginAccount(login);
            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<ApiResponse<string>>> RegisterUser([FromQuery]RegisterUserDto input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userAuthServices.RegisterUser(input);
            return Ok(result);
        }
    }
}
