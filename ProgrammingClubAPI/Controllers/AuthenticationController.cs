using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingClubAPI.Models.AuthenticationDTOs;
using ProgrammingClubAPI.Services;

namespace ProgrammingClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(ITokenService tokenService, UserManager<IdentityUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Username
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                if (user.Roles != null && user.Roles.Length > 0)
                {
                    foreach (var role in user.Roles)
                    {
                        await _userManager.AddToRoleAsync(identityUser, role);
                    }
                }
                return Ok(new { message = "USer registered" });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var identityUser = await _userManager.FindByNameAsync(loginRequest.Username);
            if (identityUser != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(identityUser, loginRequest.Password);
                if (checkPassword)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    var token = _tokenService.CreateToken(identityUser, roles.ToList());
                    var response = new LoginRequestDto
                    {
                        Token = token
                    };
                    return Ok(response);
                }
            }
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }

}