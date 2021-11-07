﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OngProject.Application.DTOs.Identity;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Identity;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Identity
{
    public class IdentityService : IIdentityService
    {
        private const string DefaultRole = "User";
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(UserManager<ApplicationUser> userManager, ITokenHandlerService tokenHandlerService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenHandlerService = tokenHandlerService;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<string> Register(AuthRequestDto requestDto)
        {
            var userExist = await _userManager.FindByEmailAsync(requestDto.Email);

            if (userExist is not null)
                throw new BadRequestException("The email already exist.");

            var newUser = new ApplicationUser
            {
                Email = requestDto.Email,
                UserName = requestDto.Email
            };

            var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

            if (!isCreated.Succeeded)
                throw new BadRequestException(isCreated.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(newUser, DefaultRole);
            
            // Create the user on UserDetails table
            var user = new UserDetails {IdentityId = new Guid(newUser.Id)};

            await _unitOfWork.UsersDetails.Create(user);
            await _unitOfWork.CompleteAsync();

            return newUser.Email;
        }

        public async Task<AuthResponseDto> Login(AuthRequestDto requestDto)
        {
            var userExist = await _userManager.FindByEmailAsync(requestDto.Email);

            if (userExist is null)
                throw new BadRequestException("Incorrect email or password.");

            var isCorrect = await _userManager.CheckPasswordAsync(userExist, requestDto.Password);

            if (isCorrect)
            {
                var role = await _userManager.GetRolesAsync(userExist);

                var parameters = new TokenParameters
                {
                    Id = userExist.Id,
                    UserName = userExist.UserName,
                    Role = role.First()
                };

                var jwtToken = _tokenHandlerService.GenerateJwtToken(parameters);

                return new AuthResponseDto
                {
                    Login = true,
                    Token = jwtToken
                };
            }

            throw new BadRequestException("Incorrect email or password.");
        }
    }
}