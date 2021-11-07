using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Identity;
using OngProject.Application.Interfaces.Identity;
using Swashbuckle.AspNetCore.Annotations;
using OngProject.Services;
using System.Security.Claims;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        [HttpPost("register")]
        #region Documentation
        [SwaggerOperation(Summary = "Register user",Description = "Register user")]
        [SwaggerResponse(200, "Success. Returns a username")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        #endregion
        public async Task<ActionResult<string>> Register([FromBody] AuthRequestDto requestDto)
        {
            return await _identityService.Register(requestDto);
        }
        
        #region Documentation
        [SwaggerOperation(Summary = "User Login")]
        [SwaggerResponse(200, "Logged in. Returns Token")]
        [SwaggerResponse(400, "Incorrect email or password.")]
        #endregion
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] AuthRequestDto requestDto)
        {
            return await _identityService.Login(requestDto);
        }

        [HttpGet("me")]
        public ActionResult<CurrentUserService> Me(){
            var c = new CurrentUserService();
           
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            c.userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            c.userName = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            c.userRole = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
           
            return c;
        }


    }
}