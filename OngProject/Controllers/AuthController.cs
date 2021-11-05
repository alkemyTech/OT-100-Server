using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Identity;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Identity;
using OngProject.DataAccess.Identity;
using OngProject.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            ITokenHandlerService tokenHandlerService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenHandlerService = tokenHandlerService;
            _unitOfWork = unitOfWork;
        }
        
        #region Documentation
        [SwaggerOperation(Summary = "User Login")]
        [SwaggerResponse(200, "Logged in. Returns Token")]
        [SwaggerResponse(400, "Incorrect email or password.")]
        #endregion
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
                    // If the user is correct, search the role where it belongs
                    var role = await _userManager.GetRolesAsync(userExist);
                    
                    var parameters = new TokenParameters
                    {
                        Id = userExist.Id,
                        UserName = userExist.UserName,
                        Role = role.First()
                    };

                    var jwtToken = _tokenHandlerService.GenerateJwtToken(parameters);

                    return Ok(new Result
                    {
                        Login = true,
                        Token = jwtToken
                    });
                }

                return BadRequest(new Result
                {
                    Login = false,
                    Errors = new List<string> {"Incorrect email or password."}
                });
            }

            return BadRequest(new Result
            {
                Login = false,
                Errors = new List<string> {"Incorrect email or password."}
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        #region Documentation
        [SwaggerOperation(Summary = "Register user",Description = "Register user")]
        [SwaggerResponse(200, "Success. Returns a username")]
        [SwaggerResponse(400, "BadRequest. Something went wrong, try again")]
        #endregion
        public async Task<ActionResult> Register([FromBody] UserRegistrationRequestDto userRegDto)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(userRegDto.Email);

                if (userExist != null)
                    return BadRequest("The Email already exist.");

                // exist user role
                var role = await _roleManager.FindByNameAsync("User");

                if (role == null)
                    return BadRequest("Error when registering user."); //Create the role user first


                var newUser = new IdentityUser
                {
                    Email = userRegDto.Email,
                    UserName = userRegDto.Email,
                    EmailConfirmed = true
                };


                var isCreated = await _userManager.CreateAsync(newUser, userRegDto.Password);


                if (!isCreated.Succeeded)
                    return BadRequest(isCreated.Errors.Select(e => e.Description));

                // Add IdentityRole and FindId to RoleExtension
                await _userManager.AddToRoleAsync(newUser, role.Name);

                var roleEx = await _unitOfWork.Roles.GetByGuid(new Guid(role.Id));


                // Create the user on user table
                var user = new User()
                {
                    IdentityId = new Guid(newUser.Id),
                    FirstName = userRegDto.FirstName,
                    LastName = userRegDto.LastName,
                    Email = userRegDto.Email,
                    Password = newUser.PasswordHash,
                    RoleId = roleEx.Id
                };

                await _unitOfWork.Users.Create(user);
                await _unitOfWork.CompleteAsync();

                return Ok(newUser.Email);
            }
            else
                return BadRequest("Error when registering user.");
        }
    }
}