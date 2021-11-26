using AutoMapper;
using OngProject.Application.DTOs.UsersDetails;
using OngProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Application.DTOs.Mails;
using System.IO;
using OngProject.Application.Interfaces.Mail;
using OngProject.Application.Interfaces.Identity;
using OngProject.Application.Mappings;

namespace OngProject.Application.Services
{
    public class UserDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IMailService _mailService;

        public UserDetailsService(IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
            _identityService = identityService;
        }

        public async Task<List<GetUsersDetailsDto>> GetUsers()
        {
            var users = await _unitOfWork.UsersDetails.GetAll();

            return users
                .AsQueryable()
                .ProjectToList<GetUsersDetailsDto>(_mapper.ConfigurationProvider);
        }

        public async Task<GetUserDetailsDto> GetUserDetails(int id)
        {
            var users = await _unitOfWork.UsersDetails.GetById(id);

            if (users is null)
                throw new NotFoundException(nameof(UserDetails), id);

            return _mapper.Map<GetUserDetailsDto>(users);
        }

        public async Task PatchUser(int id, JsonPatchDocument<UpdateUserDetailsDto> patchDocument)
        {
            var user = await _unitOfWork.UsersDetails.GetById(id);
            
            if (user is null)
                throw new NotFoundException(nameof(UserDetails), id);

            var userDto = _mapper.Map<UpdateUserDetailsDto>(user);
            
            patchDocument.ApplyTo(userDto);
            
            await _unitOfWork.UsersDetails.Update(_mapper.Map(userDto, user));
            await _unitOfWork.CompleteAsync();
        }

        public async Task SoftDeleteUsers(int id)
        {
            var user = await _unitOfWork.UsersDetails.GetById(id);
            
            if (user is null)
                throw new NotFoundException(nameof(UserDetails), id);
            
            var userEmail = await _identityService.GetEmail(user.IdentityId.ToString());
            
            var mail = new SendMailDto
            {
                Name = user.FirstName + user.LastName,
                EmailTo = userEmail,
                Subject = "Baja ONG Somos más.",
                Text = await File.ReadAllTextAsync("./wwwroot/templates/plantilla_email_baja.html")
            };

            await _mailService.SendMail(mail);
            await _unitOfWork.UsersDetails.Delete(user);
            await _unitOfWork.CompleteAsync();
        }
    }

}
