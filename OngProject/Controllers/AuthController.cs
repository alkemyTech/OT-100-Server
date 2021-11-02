using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OngProject.Application.DTOs.Identity;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Identity;
using OngProject.DataAccess.Identity;
using OngProject.Domain.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService tokenHandlerService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenHandlerService = tokenHandlerService;
            _unitOfWork = unitOfWork;
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

         [HttpPost("register")]
         public async Task<ActionResult> Register([FromBody] UserRegistrationRequestDto userRegDto)
         {
             if (ModelState.IsValid)
             {
                 var userExist = await _userManager.FindByEmailAsync(userRegDto.Email);
                 if (userExist != null)
                 {
                     return BadRequest(new Result
                     {
                         Errors = new List<string> {"The Email already exist."}
                     });
                 }

                 var newUser = new IdentityUser()
                 {
                     Email = userRegDto.Email,
                     UserName = userRegDto.Email,
                     EmailConfirmed = true
                 };

                 var isCreated = await _userManager.CreateAsync(newUser, userRegDto.Password);

                 if (!isCreated.Succeeded)
                 {
                     return BadRequest(new Result
                     {
                         Errors = isCreated.Errors.Select(x => x.Description).ToList()
                     });
                 }
                 
                 // Create the user on user table
                 var user = new User
                 {
                    IdentityId = new Guid(newUser.Id),
                    FirstName = userRegDto.FirstName,
                    LastName = userRegDto.LastName,
                    Email = userRegDto.Email,
                    Password = newUser.PasswordHash,
                    RoleId = 1 // Role Usuarios
                 };

                 await _unitOfWork.Users.Create(user);
                 await _unitOfWork.CompleteAsync();
                 
                 
                 var parameters = new TokenParameters
                 {
                     Id = newUser.Id,
                     UserName = newUser.UserName,
                     PasswordHash = newUser.PasswordHash
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
                     Errors = new List<string> {"Invalid Model."}
                 });
             }

         }
    }
}