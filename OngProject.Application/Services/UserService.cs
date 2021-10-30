﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Users;
using OngProject.DataAccess.Interfaces;
using OngProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Application.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUsersDto>> GetUsers()
        {
            var members = await _unitOfWork.Users.GetAll();

            return members
                .AsQueryable()
                .ProjectTo<GetUsersDto>(_mapper.ConfigurationProvider);
        }

        public async Task<int> CreateUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _unitOfWork.Users.Create(user);
            await _unitOfWork.CompleteAsync();

            return user.Id;
        }

        public async Task<GetUserDetailsDto> GetUserDetails(int id)
        {
            var users = await _unitOfWork.Users.GetById(id);

            if (users is null)
                throw new Exception($"No se encontró la entidad {nameof(User)} de id ({id}).");

            return _mapper.Map<GetUserDetailsDto>(users);
        }

        public async Task UpdateUser(int id, CreateUserDto usersDto)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user is null)
                throw new Exception($"No se encontró la entidad {nameof(User)} de id ({id}).");

            user.Id = id;
            await _unitOfWork.Users.Update(_mapper.Map(usersDto, user));
            await _unitOfWork.CompleteAsync();
        }

        public async Task SoftDeleteUsers(int id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user is null)
                throw new Exception($"No se encontró la entidad {nameof(User)} de id ({id}).");

            await _unitOfWork.Users.Delete(user);
            await _unitOfWork.CompleteAsync();
        }
    }

}
