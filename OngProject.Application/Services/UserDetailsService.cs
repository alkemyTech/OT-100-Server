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

namespace OngProject.Application.Services
{
    public class UserDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserDetailsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUsersDetailsDto>> GetUsers()
        {
            var users = await _unitOfWork.UsersDetails.GetAll();

            return users
                .AsQueryable()
                .ProjectTo<GetUsersDetailsDto>(_mapper.ConfigurationProvider);
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

            await _unitOfWork.UsersDetails.Delete(user);
            await _unitOfWork.CompleteAsync();
        }
    }

}
