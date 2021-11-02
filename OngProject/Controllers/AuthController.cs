using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Identity;
using OngProject.Application.Interfaces.Identity;
using OngProject.DataAccess.Identity;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService _tokenHandlerService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService tokenHandlerService)
        {
            _userManager = userManager;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginRequestDto loginDto)
        {
            if (ModelState.IsValid)
            {
                // Check if email exist
                var userExist = await _userManager.FindByEmailAsync(loginDto.Email);

                if (userExist is null)
                {
                    return BadRequest(new Result
                    {
                        Login = false,
                        Errors = new List<string> {"Incorrect email or password."}
                    });
                }
                
                // Check if the user has a valid password
                var isCorrect = await _userManager.CheckPasswordAsync(userExist, loginDto.Password);

                if (isCorrect)
                {
                    var parameters = new TokenParameters
                    {
                        Id = userExist.Id,
                        UserName = userExist.UserName,
                        PasswordHash = userExist.PasswordHash
                    };

                    var jwtToken = _tokenHandlerService.GenerateJwtToken(parameters);

                    return Ok(new Result
                    {
                        Login = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new Result
                    {
                        Login = false,
                        Errors = new List<string> {"Incorrect email or password."}
                    });
                }
            }
            else
            {
                return BadRequest(new Result
                {
                    Login = false,
                    Errors = new List<string> {"Incorrect email or password."}
                });
            }
        }
    }
}