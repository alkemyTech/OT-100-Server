using System.Net.Mime;
using AutoMapper;
using Moq;
using OngProject.Application.DTOs.Testimonials;
using OngProject.Application.Interfaces;
using OngProject.Application.Services;
using OngProject.Controllers;
using OngProject.Domain.Entities;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using OngProject.Application.Mappings;
using OngProject.Application.Exceptions;

namespace UnitTests
{
    public class TestimonialsControllerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly MapperConfiguration _configuration =
            new(config => config.AddProfile<MappingProfile>());

        public TestimonialsControllerTest()
        {
            Service = new TestimonyService(_unitOfWork.Object, _configuration.CreateMapper());
            Controller = new TestimonialsController(Service);
        }

        private TestimonyService Service { get; set; }
        private TestimonialsController Controller { get; set; }

        #region Create
        [Fact]
        public async Task CreateTestimonials_ReturnsId()
        {
            //Arrange
            var testimony = new Testimony
            {
                Id = 1,
                Name = "testimonio",
                Image = "img",
                Content = "contenido"
            };

            var testimonyDto = new CreateTestimonyDto
            {
                Name = "testimonio",
                Image = "img",
                Content = "contenido"
            };

            
            _unitOfWork.Setup(t => t.Testimonials.Create(testimony)).Verifiable("Created");
            _unitOfWork.Setup(t => t.CompleteAsync()).Verifiable("Completed");

            //Act
            var result = await Controller.Create(testimonyDto);

            //Assert
            Assert.Equal(0, result.Value);
        }
        #endregion

        #region Delete
        [Fact]
        public void DeleteTestimonials_WithExistingId_NoReturns()
        {
            //Arrange
            var testimonials = new List<Testimony>
            {
                new() {Id = 1, Name = "testimonio1", Image = "img1", Content = "contenido1"},
                new() {Id = 2, Name = "testimonio2", Image = "img2", Content = "contenido2"},
                new() {Id = 3, Name = "testimonio3", Image = "img3", Content = "contenido3"}

            };
           
            const int idToDelete = 2;

            _unitOfWork.Setup(t => t.Testimonials.GetById(idToDelete))
            .ReturnsAsync(testimonials.Single(t => t.Id == idToDelete));

            _unitOfWork.Setup(t => t.Testimonials.Delete(It.IsAny<Testimony>()))
            .Callback<Testimony>((entity) => testimonials.Remove(entity));

            _unitOfWork.Setup(t => t.CompleteAsync()).Verifiable("Completed");

            //Act
            var result = Controller.Delete(idToDelete);

            //Assert
            Assert.Equal(2, testimonials.Count);
            _unitOfWork.Verify(u => u.Testimonials.GetById(idToDelete), Times.Once());
            _unitOfWork.Verify(u => u.Testimonials.Delete(It.IsAny<Testimony>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());

        }

        [Fact]
        public async Task DeleteTestimonials_WithUnexistingId_ReturnsNotFound()
        {
            // Arrange
              var testimonials = new List<Testimony>
            {
                new() {Id = 1, Name = "testimonio1", Image = "img1", Content = "contenido1"},
                new() {Id = 2, Name = "testimonio2", Image = "img2", Content = "contenido2"},
                new() {Id = 3, Name = "testimonio3", Image = "img3", Content = "contenido3"}

            };
            
            const int idToDelete = 4;

            _unitOfWork.Setup(u => u.Testimonials.GetById(idToDelete))
                .ReturnsAsync(testimonials.SingleOrDefault(t => t.Id == idToDelete));

            _unitOfWork.Setup(u => u.Testimonials.Delete(It.IsAny<Testimony>()))
                .Callback<Testimony>(t => testimonials.Remove(t));
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Complete");

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.Delete(idToDelete));
        }
        #endregion

        #region GetAll
        [Fact]
        public async Task GetAllTestimonials_ReturnTestimonials()
        {
            //Arrange
            var testimonials = new List<Testimony>
            {
                new() {Id = 1, Name = "testimonio1", Image = "img1", Content = "contenido1"},
                new() {Id = 2, Name = "testimonio2", Image = "img2", Content = "contenido2"},
                new() {Id = 3, Name = "testimonio3", Image = "img3", Content = "contenido3"}

            };
           
           var pagination = new TestimonialsQueryDto{
               PageNumber = 1,
               PageSize = 10
           };

            _unitOfWork.Setup(t => t.Testimonials.GetAll())
            .ReturnsAsync(testimonials);


            //Act
            var result = await Controller.GetAll(pagination);

            //Assert
            Assert.Equal(testimonials.Count, result.Value.Items.Count);
            _unitOfWork.Verify(u => u.Testimonials.GetAll(), Times.Once());
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetByIdTestimonials_Returns()
        {
            //Arrange
            var testimonials = new List<Testimony>
            {
                new() {Id = 1, Name = "testimonio1", Image = "img1", Content = "contenido1"},
                new() {Id = 2, Name = "testimonio2", Image = "img2", Content = "contenido2"},
                new() {Id = 3, Name = "testimonio3", Image = "img3", Content = "contenido3"}

            };
           
             var idToFind = 2;

            _unitOfWork.Setup(t => t.Testimonials.GetById(idToFind))
            .ReturnsAsync(testimonials.Single(t => t.Id == idToFind));

            //Act
            var result = await Controller.GetById(idToFind);

            //Assert
            Assert.Equal(idToFind, result.Value.Id);
            _unitOfWork.Verify(u => u.Testimonials.GetById(idToFind), Times.Once());
        }
        #endregion

        #region Update

         [Fact]
        public async Task UpdateTestimonials_WithExistingEntity_NoReturns()
        {
            // Arrange
             var testimonials = new List<Testimony>
            {
                new() {Id = 1, Name = "testimonio1", Image = "img1", Content = "contenido1"},
                new() {Id = 2, Name = "testimonio2", Image = "img2", Content = "contenido2"},
                new() {Id = 3, Name = "testimonio3", Image = "img3", Content = "contenido3"}

            };

            var testimonyDto = new CreateTestimonyDto
            {
                Name = "testimonio edit",
                Image = "img",
                Content = "contenido"
            };
 
            var id = 2;
             
            _unitOfWork.Setup(t => t.Testimonials.GetById(id))
            .ReturnsAsync(testimonials.SingleOrDefault(t => t.Id == id));

            _unitOfWork.Setup(u => u.Testimonials.Update(It.IsAny<Testimony>())).Verifiable("Updated");
            _unitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Completed");
 
            // Act
            await Controller.Update(id,testimonyDto);
 
            // Assert
            _unitOfWork.Verify(u => u.Testimonials.Update(It.Is<Testimony>(t => t.Id == id && 
            t.Name == testimonyDto.Name && t.Image == testimonyDto.Image && t.Content == testimonyDto.Content )));
        }

          [Fact]
        public async Task UpdateTestimonials_WithUnexistingEntity_ReturnsNotFound()
        {
            // Arrange
              var testimonials = new List<Testimony>
            {
                new() {Id = 1, Name = "testimonio1", Image = "img1", Content = "contenido1"},
                new() {Id = 2, Name = "testimonio2", Image = "img2", Content = "contenido2"},
                new() {Id = 3, Name = "testimonio3", Image = "img3", Content = "contenido3"}

            };
            
            const int id = 4;
            
               var testimonyDto = new CreateTestimonyDto
            {
                Name = "testimonio edit",
                Image = "img",
                Content = "contenido"
            };

              _unitOfWork.Setup(t => t.Testimonials.GetById(id))
            .ReturnsAsync(testimonials.SingleOrDefault(t => t.Id == id));
            

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => Controller.Update(id, testimonyDto));
        }
        #endregion
    }
}