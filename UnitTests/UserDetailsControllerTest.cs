using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OngProject.Application.DTOs.Mails;
using OngProject.Application.DTOs.UsersDetails;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Identity;
using OngProject.Application.Interfaces.Mail;
using OngProject.Application.Mappings;
using OngProject.Application.Services;
using OngProject.Controllers;
using OngProject.DataAccess.Identity;
using OngProject.Domain.Entities;
using Xunit;

namespace UnitTests
{
    public class UserDetailsControllerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly MapperConfiguration _configuration =
            new(config => config.AddProfile<MappingProfile>());

        private readonly Mock<IIdentityService> _identityService = new();
        private readonly Mock<IMailService> _mailService = new();

        public UserDetailsControllerTest()
        {
            Service = new UserDetailsService(_unitOfWork.Object, _configuration.CreateMapper(), _mailService.Object,
                _identityService.Object);
            Controller = new UsersDetailsController(Service);
        }

        private UserDetailsService Service { get; set; }
        private UsersDetailsController Controller { get; set; }


        #region Get All
        [Fact]
        public async Task GetAll_WithExistingUsers_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<UserDetails>
            {
                new() {Id = 1, IdentityId = Guid.NewGuid(), FirstName = "Esteban", LastName = "Quito", Photo = "Image.png"},
                new() {Id = 2, IdentityId = Guid.NewGuid(), FirstName = "Aquiles", LastName = "Boy", Photo = "Image.png"},
                new() {Id = 3, IdentityId = Guid.NewGuid(), FirstName = "Alan", LastName = "Brito", Photo = "Image.png"}
            };

            _unitOfWork.Setup(u => u.UsersDetails.GetAll()).ReturnsAsync(users);
            
            // Act
            var result = await Controller.GetAll();

            // Assert
            Assert.IsType<List<GetUsersDetailsDto>>(result.Value);
            Assert.Equal(users.Count, result.Value.Count);
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetById_WithExistingUser_ReturnsUser()
        {
            // Arrange
            var users = new List<UserDetails>
            {
                new() {Id = 1, IdentityId = Guid.NewGuid(), FirstName = "Esteban", LastName = "Quito", Photo = "Image.png"},
                new() {Id = 2, IdentityId = Guid.NewGuid(), FirstName = "Aquiles", LastName = "Boy", Photo = "Image.png"}
            };
            
            const int id = 2;

            _unitOfWork.Setup(u => u.UsersDetails.GetById(id))
                .ReturnsAsync(users.SingleOrDefault(u => u.Id == id));

            // Act
            var result = await Controller.GetById(id);

            // Assert
            Assert.IsType<GetUserDetailsDto>(result.Value);
            result.Value.Should().BeEquivalentTo(users.Single(u => u.Id == id), options =>
                    options.ComparingByMembers<GetUserDetailsDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetById_WithUnexistingUser_ReturnsNotFound()
        {
            // Arrange
            var users = new List<UserDetails>
            {
                new() {Id = 1, IdentityId = Guid.NewGuid(), FirstName = "Esteban", LastName = "Quito", Photo = "Image.png"}
            };

            const int id = 2;

            _unitOfWork.Setup(u => u.UsersDetails.GetById(id))
                .ReturnsAsync(users.SingleOrDefault(u => u.Id == id));

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.GetById(id));
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_WithExistingUser_NoReturns()
        {
            // Arrange
            const int idToUpdate = 1;
            
            var users = new List<UserDetails>
            {
                new() {Id = 1, IdentityId = Guid.NewGuid(), FirstName = "Esteban", LastName = "Quito", Photo = "Image.png"},
                new() {Id = 2, IdentityId = Guid.NewGuid(), FirstName = "Aquiles", LastName = "Boy", Photo = "Image.png"}
            };

            var userDto = new UpdateUserDetailsDto {FirstName = string.Empty, LastName = string.Empty};

            _unitOfWork.Setup(u => u.UsersDetails.GetById(idToUpdate))
                .ReturnsAsync(users.SingleOrDefault(u => u.Id == idToUpdate));
            
            _unitOfWork.Setup(u => u.UsersDetails.Update(It.IsAny<UserDetails>())).Verifiable("Updated");
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Completed");
            
            JsonPatchDocument<UpdateUserDetailsDto> patchDocument = new();
            patchDocument.Replace(p => p.FirstName, "NewFirstName");
            patchDocument.Replace(p => p.LastName, "NewLastName");
            patchDocument.ApplyTo(userDto);
            
            // Act
            await Controller.Patch(idToUpdate, patchDocument);
            
            // Assert
            var firstName = users.Single(u => u.Id == idToUpdate).FirstName;
            var lastName = users.Single(u => u.Id == idToUpdate).LastName;
            
            Assert.Equal("NewFirstName", userDto.FirstName);
            Assert.Equal("NewLastName", userDto.LastName);
            Assert.Equal(firstName, userDto.FirstName);
            Assert.Equal(lastName, userDto.LastName);
        }

        [Fact]
        public async Task Update_WithUnexistingUser_ReturnsNotFound()
        {
            // Arrange
            const int idToUpdate = 2;
            
            var users = new List<UserDetails>
            {
                new() {Id = 1, IdentityId = Guid.NewGuid(), FirstName = "Esteban", LastName = "Quito", Photo = "Image.png"}
            };
            
            var userDto = new UpdateUserDetailsDto {FirstName = string.Empty, LastName = string.Empty};
            
            _unitOfWork.Setup(u => u.UsersDetails.GetById(idToUpdate))
                .ReturnsAsync(users.SingleOrDefault(u => u.Id == idToUpdate));
            
            _unitOfWork.Setup(u => u.UsersDetails.Update(It.IsAny<UserDetails>())).Verifiable("Updated");
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Completed");
            
            JsonPatchDocument<UpdateUserDetailsDto> patchDocument = new();
            patchDocument.Replace(p => p.FirstName, "NewFirstName");
            patchDocument.Replace(p => p.LastName, "NewLastName");
            patchDocument.ApplyTo(userDto);
            
            // Act
            
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.Patch(idToUpdate, patchDocument));
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_WithExistingUser_NoReturns()
        {
            // Arrange
            var users = new List<UserDetails>
            {
                new() {Id = 1, IdentityId = Guid.NewGuid(), FirstName = "Esteban", LastName = "Quito", Photo = "Image.png"},
                new() {Id = 2, IdentityId = Guid.NewGuid(), FirstName = "Aquiles", LastName = "Boy", Photo = "Image.png"},
                new() {Id = 3, IdentityId = Guid.NewGuid(), FirstName = "Alan", LastName = "Brito", Photo = "Image.png"}
            };

            const int idToDelete = 2;

            _unitOfWork.Setup(u => u.UsersDetails.GetById(idToDelete))
                .ReturnsAsync(users.SingleOrDefault(u => u.Id == idToDelete));

            _unitOfWork.Setup(u => u.UsersDetails.Delete(It.IsAny<UserDetails>()))
                .Callback<UserDetails>(entity => users.Remove(entity));
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Completed");
            
            _mailService.Setup(m => m.SendMail(It.IsAny<SendMailDto>())).Verifiable("Sent");

            // Act
            await Controller.Delete(idToDelete);

            // Assert
            Assert.Equal(2, users.Count);
            _unitOfWork.Verify(u => u.UsersDetails.GetById(idToDelete), Times.Once());
            _unitOfWork.Verify(u => u.UsersDetails.Delete(It.Is<UserDetails>(m => m != null)), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
        }

        [Fact]
        public async Task Delete_WithUnexistingUser_ReturnsNotFound()
        {
            // Arrange
            var users = new List<UserDetails>
            {
                new() {Id = 1, IdentityId = Guid.NewGuid(), FirstName = "Esteban", LastName = "Quito", Photo = "Image.png"},
                new() {Id = 3, IdentityId = Guid.NewGuid(), FirstName = "Alan", LastName = "Brito", Photo = "Image.png"}
            };

            const int idToDelete = 4;

            _unitOfWork.Setup(u => u.UsersDetails.GetById(idToDelete))
                .ReturnsAsync(users.SingleOrDefault(u => u.Id == idToDelete));

            _unitOfWork.Setup(u => u.UsersDetails.Delete(It.IsAny<UserDetails>()))
                .Callback<UserDetails>(entity => users.Remove(entity));
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Completed");
            
            _mailService.Setup(m => m.SendMail(It.IsAny<SendMailDto>())).Verifiable("Sent");

            // Act
            
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.Delete(idToDelete));
        }
        #endregion
    }
}