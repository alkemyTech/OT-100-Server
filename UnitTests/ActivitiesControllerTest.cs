using AutoMapper;
using Moq;
using OngProject.Application.Interfaces;
using OngProject.Application.Mappings;
using OngProject.Application.Services;
using OngProject.Controllers;
using OngProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using OngProject.Application.DTOs.Activities;
using OngProject.Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using OngProject.Application.Exceptions;

namespace UnitTests
{

    public class ActivitiesControllerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly MapperConfiguration _mapper;

        public ActivitiesControllerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mapper = new(config => config.AddProfile<MappingProfile>());
            Service = new ActivityService(_mockUnitOfWork.Object, _mapper.CreateMapper());
            Controller = new ActivitiesController(Service);
        }

        #region Property  
        private ActivityService Service { get; set; }
        private ActivitiesController Controller { get; set; }

        #endregion

        #region Get All
        [Fact]
        public async Task GetAllActivities_ListActivities_ActivityExists()
        {
            // Arrange
            var activities = GetSampleActivity();

            _mockUnitOfWork.Setup(c => c.Activities.GetAll()).ReturnsAsync(activities);

            // Act
            var actionResult = await Controller.GetAll();
            var result = actionResult.Value;

            // Assert
            Assert.IsType<List<GetActivitiesDto>>(result);
            Assert.Equal(GetSampleActivity().Count(), actionResult.Value.Count());
        }
        #endregion

        #region Get By Id
        [Fact]
        public async Task GetActivityById_Activity_ActivitywithSpecificeIdExists()
        {
            // Arrange
            var activities = GetSampleActivity();
            var firstActivity = activities[0];
            _mockUnitOfWork.Setup(c => c.Activities.GetById(1)).ReturnsAsync(firstActivity);

            // Act
            var actionResult = await Controller.GetById(1);
            //var result = actionResult.Result as OkObjectResult;

            // Assert

            Assert.IsType<GetActivityDetailsDto>(actionResult.Value);
            actionResult.Value.Should().BeEquivalentTo(activities.Single(e => e.Id == 1), options =>
                options.ComparingByMembers<GetActivityDetailsDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetActivityById_Activity_ActivitywithSpecificeIdNotExists()
        {
            // Arrange
            var activities = GetSampleActivity();
            //var firstActivity = activities[0];
            _mockUnitOfWork.Setup(c => c.Activities.GetById(1))
                .ReturnsAsync(activities.SingleOrDefault(m => m.Id == 1));

            // Act
            var actionResult = Controller.GetById(5);

            // Assert

            await Assert.ThrowsAsync<NotFoundException>(() => actionResult);
        }
        #endregion

        #region Create
        [Fact]
        public async Task CreateActivity_ValidActivity_ReturnsCreatedActivity()
        {
            // Arrange
            var activities = GetSampleActivity();

            var activityDto = new CreateActivityDto
            {
                Name = "newActivity",
                Content = "newContent",
                Image = "newImage.png"
            };

            _mockUnitOfWork.Setup(s => s.Activities.Create(It.IsAny<Activity>())).Verifiable("Created");
            _mockUnitOfWork.Setup(s => s.CompleteAsync()).Verifiable("Completed");

            // Act
            var actionResult = await Controller.Create(activityDto);

            // Assert
            var result = actionResult.Value;
            Assert.Equal(new int(), result);
            _mockUnitOfWork.Verify(v => v.Activities.Create(It.Is<Activity>(
                a => a.Name == activityDto.Name && a.Content == activityDto.Content && a.Image == activityDto.Image)), Times.Once());
            _mockUnitOfWork.Verify(v => v.CompleteAsync(), Times.Once);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateActivity_ValidActivity_NoReturns()
        {
            // Arrange
            var activities = GetSampleActivity();

            var activityDto = new CreateActivityDto
            {
                Name = "updateName",
                Content = "UpdateContent",
                Image = "updateImage.png"
            };

            _mockUnitOfWork.Setup(u => u.Activities.GetById(2))
                .ReturnsAsync(activities.SingleOrDefault(a => a.Id == 2));

            _mockUnitOfWork.Setup(u => u.Activities.Update(It.IsAny<Activity>())).Verifiable("Updated");
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).Verifiable("Completed");

            // Act
            var result = await Controller.Update(2, activityDto);
            
            // Assert
            _mockUnitOfWork.Verify(u => u.Activities.Update(It.Is<Activity>(a =>
                 a.Id == 2 && a.Name == activityDto.Name && a.Content == activityDto.Content && a.Image == activityDto.Image)), Times.Once);
         
        }

        [Fact]
        public async Task UpdateActivity_ValidActivity_NotFound()
        {
            // Arrange
            var activities = GetSampleActivity();

            var activityDto = new CreateActivityDto
            {
                Name = "updateName",
                Content = "UpdateContent",
                Image = "updateImage.png"
            };

            _mockUnitOfWork.Setup(s => s.Activities.GetById(5))
                .ReturnsAsync(activities.SingleOrDefault(a => a.Id == 5));

            // Act
            var result = Controller.Update(5, activityDto);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);
        }

        #endregion

        #region Delete
        [Fact]
        public async Task DeleteActivity_ValidActivity_NoReturns()
        {
            // Arrange
            var activities = GetSampleActivity();

            var activityId = 2;

            _mockUnitOfWork.Setup(s => s.Activities.GetById(activityId))
                .ReturnsAsync(activities.SingleOrDefault(m => m.Id == activityId));

            _mockUnitOfWork.Setup(s => s.Activities.Delete(It.IsAny<Activity>()))
               .Callback<Activity>(entity => activities.Remove(entity));

            _mockUnitOfWork.Setup(s => s.CompleteAsync()).Verifiable("Completed");

            // Act
            var data = await Controller.Delete(activityId);

            // Assert
            Assert.Equal(2, activities.Count());
            _mockUnitOfWork.Verify(v => v.Activities.GetById(activityId), Times.Once());
            _mockUnitOfWork.Verify(v => v.Activities.Delete(It.Is<Activity>(m => m != null)), Times.Once());
            _mockUnitOfWork.Verify(v => v.CompleteAsync(), Times.Once());
        }

        [Fact]
        public async Task DeleteActivity_ValidActivity_NotFound()
        {
            // Arrange
            var activities = GetSampleActivity();

            var activityId = 5;

            _mockUnitOfWork.Setup(s => s.Activities.GetById(activityId))
                .ReturnsAsync(activities.SingleOrDefault(a => a.Id == activityId));

            _mockUnitOfWork.Setup(s => s.Activities.Delete(It.IsAny<Activity>()))
                .Callback<Activity>(entity => activities.Remove(entity));

            _mockUnitOfWork.Setup(s => s.CompleteAsync()).Verifiable("Completed");


            // Act
            var actionResult = Controller.Delete(activityId);
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => actionResult);
        }
        #endregion


        private List<Activity> GetSampleActivity()
        {
            var activities = new List<Activity>
            {
                new ()
                {
                    Id = 1,
                    Name = "ActName1",
                    Content = "Content1",
                    Image = "Image1.png"
                },
                 new ()
                {
                    Id = 2,
                    Name = "ActName2",
                    Content = "Content2",
                    Image = "Image2.png"
                },
                 new ()
                {
                    Id = 3,
                    Name = "ActName3",
                    Content = "Content3",
                    Image = "Image3.png"
                }
            };
            return activities;
        }
    }
}