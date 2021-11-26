using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using OngProject.Application.DTOs.Members;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Application.Mappings;
using OngProject.Application.Services;
using OngProject.Controllers;
using OngProject.Domain.Entities;
using Xunit;

namespace UnitTests
{
    public class MembersControllerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly MapperConfiguration _configuration =
            new(config => config.AddProfile<MappingProfile>());

        public MembersControllerTest()
        {
            Service = new MemberService(_unitOfWork.Object, _configuration.CreateMapper());
            Controller = new MembersController(Service);
        }

        private MemberService Service { get; set; }
        private MembersController Controller { get; set; }


        #region Get All
        [Fact]
        public async Task GetAll_WithExistingMembers_ReturnsAllMembers()
        {
            // Arrange
            var members = new List<Member>
            {
                new() {Id = 1, Name = "A", Image = "image.png"},
                new() {Id = 2, Name = "B", Image = "image.png"},
                new() {Id = 3, Name = "C", Image = "image.png"}
            };

            var pagination = new MemberQueryDto {PageNumber = 1, PageSize = 10};
            
            _unitOfWork.Setup(u => u.Members.GetAll()).ReturnsAsync(members);

            // Act
            var result = await Controller.GetAll(pagination);

            // Assert
            Assert.IsType<List<GetMembersDto>>(result.Items);
            Assert.Equal(members.Count, result.Items.Count);
        }
        #endregion

        #region Get By Id
        [Fact]
        public async Task GetById_WithExistingMember_ReturnsMember()
        {
            // Arrange
            var members = new List<Member>
            {
                new() {Id = 1, Name = "A", Image = "Image.png"},
                new() {Id = 2, Name = "B", Image = "Image.png"}
            };

            const int id = 2;

            _unitOfWork.Setup(u => u.Members.GetById(id))
                .ReturnsAsync(members.SingleOrDefault(m => m.Id == id));

            // Act
            var result = await Controller.GetById(id);

            // Assert
            Assert.IsType<GetMembersDto>(result.Value);
            result.Value.Should().BeEquivalentTo(members.Single(e => e.Id == id), options =>
                options.ComparingByMembers<GetMembersDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetById_WithUnexistingMember_ReturnsNotFound()
        {
            // Arrange
            var members = new List<Member> {new() {Id = 1, Name = "A", Image = "Image.png"}};

            const int id = 2;
            
            _unitOfWork.Setup(u => u.Members.GetById(id))
                .ReturnsAsync(members.SingleOrDefault(m => m.Id == id));
            
            // Act
            
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.GetById(id));
        }
        #endregion

        #region Create
        [Fact]
        public async Task Create_WithMemberToCreate_ReturnsCreateMember()
        {
            var memberDto = new CreateMemberDto
            {
                Name = "A",
                Image = "."
            };
            
            _unitOfWork.Setup(s => s.Members.Create(It.IsAny<Member>())).Verifiable("Created");
            _unitOfWork.Setup(s => s.CompleteAsync()).Verifiable("Completed");
            
            // Act
            var result = await Controller.Create(memberDto);

            // Assert
            Assert.Equal(new int(), result.Value);
            _unitOfWork.Verify(s => s.Members.Create(It.Is<Member>(
                e => e.Name == memberDto.Name && e.Image == memberDto.Image)), Times.Once());
            _unitOfWork.Verify(s => s.CompleteAsync(), Times.Once);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_WithExistingMember_NoReturns()
        {
            // Arrange
            const int idToUpdate = 1;
            
            var members = new List<Member>
            {
                new() {Id = 1, Name = "A", Image = "Image.png"},
                new() {Id = 2, Name = "B", Image = "Image.png"}
            };

            var memberDto = new CreateMemberDto {Name = "Updated", Image = "NewImage.png"};

            _unitOfWork.Setup(u => u.Members.GetById(idToUpdate))
                .ReturnsAsync(members.SingleOrDefault(m => m.Id == idToUpdate));

            _unitOfWork.Setup(u => u.Members.Update(It.IsAny<Member>())).Verifiable("Updated");
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Completed");

            // Act
            var result = await Controller.Update(idToUpdate, memberDto);

            // Assert
            _unitOfWork.Verify(u => u.Members.Update(It.Is<Member>(m =>
                 m.Id == idToUpdate && m.Name == memberDto.Name && m.Image == memberDto.Image)), Times.Once);
            
        }

        [Fact]
        public async Task Update_WithUnexistingMember_ReturnsNotFound()
        {
            // Arrange
            var members = new List<Member> {new() {Id = 1, Name = "A", Image = "Image.png"}};
            
            const int idToUpdate = 2;
            
            var memberDto = new CreateMemberDto {Name = "Updated", Image = "NewImage.png"};

            _unitOfWork.Setup(u => u.Members.GetById(idToUpdate))
                .ReturnsAsync(members.SingleOrDefault(m => m.Id == idToUpdate));
            
            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.Update(idToUpdate, memberDto));
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_WithExistingMember_NoReturns()
        {
            // Arrange
            var members = new List<Member>
            {
                new() {Id = 1, Name = "A", Image = "Image.png"},
                new() {Id = 2, Name = "B", Image = "Image.png"},
                new() {Id = 3, Name = "C", Image = "Image.png"}
            };
            
            const int idToDelete = 3;

            _unitOfWork.Setup(u => u.Members.GetById(idToDelete))
                .ReturnsAsync(members.SingleOrDefault(m => m.Id == idToDelete));
            
            _unitOfWork.Setup(u => u.Members.Delete(It.IsAny<Member>()))
                .Callback<Member>(entity => members.Remove(entity));
            _unitOfWork.Setup(s => s.CompleteAsync()).Verifiable("Completed");
            
            // Act
            await Controller.Delete(idToDelete);
            
            // Assert
            Assert.Equal(2, members.Count);
            _unitOfWork.Verify(u => u.Members.GetById(idToDelete), Times.Once());
            _unitOfWork.Verify(u => u.Members.Delete(It.Is<Member>(m => m != null)), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
        }

        [Fact]
        public async Task Delete_WithUnexistingMember_ReturnsNotFound()
        {
            // Arrange
            var members = new List<Member> {new() {Id = 1, Name = "A", Image = "Image.png"}};
            
            const int idToDelete = 2;

            _unitOfWork.Setup(u => u.Members.GetById(idToDelete))
                .ReturnsAsync(members.SingleOrDefault(m => m.Id == idToDelete));

            _unitOfWork.Setup(u => u.Members.Delete(It.IsAny<Member>()))
                .Callback<Member>(m => members.Remove(m));
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Complete");

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.Delete(idToDelete));
        }
        #endregion
    }
}