﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OngProject.Application.DTOs;
using OngProject.Application.DTOs.Identity;
using OngProject.Application.DTOs.Mails;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Identity;
using OngProject.Application.Interfaces.Mail;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Identity
{
    public class IdentityService : IIdentityService
    {
        private const string DefaultRole = "User";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMailService _mailService;


        public IdentityService(UserManager<ApplicationUser> userManager, ITokenHandlerService tokenHandlerService,
            IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMailService mailService)
        {
            _userManager = userManager;
            _tokenHandlerService = tokenHandlerService;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mailService = mailService;
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
            var user = new UserDetails { IdentityId = new Guid(newUser.Id) };

            await _unitOfWork.UsersDetails.Create(user);
            await _unitOfWork.CompleteAsync();

            var mail = new SendMailDto
            {
                Name = newUser.UserName,
                EmailTo = newUser.Email,
                Subject = "Registro ONG Somos más.",
                Text = await File.ReadAllTextAsync("./wwwroot/templates/plantilla_email.html")
            };

            await _mailService.SendMail(mail);

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
        
        public async Task<CurrentUserDto> Me(){
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(_currentUserService.userId));

            if(user is null){
                throw new NotFoundException();
            }

            var userDto = new CurrentUserDto{
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
            return userDto;
        }

        public async Task<string> GetEmail(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                throw new BadRequestException("The user no exist.");
            }

            return user.Email;
        }

        public async Task<string> GetUserRol(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);

            if (user is null)
            {
                throw new BadRequestException("The user no exist.");
            }

            return role.First();
        }
        public async Task Delete(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            await _userManager.DeleteAsync(user);

        }
    }
}