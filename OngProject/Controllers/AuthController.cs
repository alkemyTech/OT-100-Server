using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Identity;
using OngProject.Application.Interfaces.Identity;
using Swashbuckle.AspNetCore.Annotations;
using OngProject.Application.DTOs;
using OngProject.Application.Exceptions;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [SwaggerTag("Login, Sign up and Account details")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        [HttpPost("register")]
        #region Documentation
        [SwaggerOperation(Summary = "Register user", Description = ".")]
        [SwaggerResponse(200, "Success. Returns a username.")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<string>> Register([FromBody] AuthRequestDto requestDto)
        {
            return await _identityService.Register(requestDto);
        }
        
        #region Documentation
        [SwaggerOperation(Summary = "User Login")]
        [SwaggerResponse(200, "Logged in. Returns Token")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] AuthRequestDto requestDto)
        {
            return await _identityService.Login(requestDto);
        }
        
        [HttpGet("me")]
        [Authorize(Roles = "User, Admin")]
        #region Documentation
        [SwaggerOperation(Summary = "Account details", Description = "Requires user or admin privileges.")]
        [SwaggerResponse(200, "Success. Return the account details.")]
        [SwaggerResponse(401, "Unauthenticated user or wrong jwt token.")]
        [SwaggerResponse(500, "Internal server error. An error occurred while processing your request.")]
        #endregion
        public async Task<ActionResult<CurrentUserDto>> Me(){
            
           return await _identityService.Me();
        }
    }
}