using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OngProject.Application.DTOs.Contacts;
using OngProject.Application.DTOs.Mails;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Mail;
using OngProject.Application.Mappings;
using OngProject.Application.Services;
using OngProject.Controllers;
using OngProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace UnitTests
{

    public class ContactsControllerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly MapperConfiguration _mapper;
        private readonly Mock<IMailService> _mailService = new();

        public ContactsControllerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mapper = new (config => config.AddProfile<MappingProfile>());

            Service = new ContactService(_mockUnitOfWork.Object, _mapper.CreateMapper(), _mailService.Object);
            Controller = new ContactsController(Service);
        }

        #region Property  
        private ContactService Service { get; set; }
        private ContactsController Controller { get; set; }

        #endregion

        #region Get All
        [Fact]
        public async Task GetAllContacts_ListContacts_ContactsExists()
        {
            // Arrange
            var contacts = GetSampleContact();

            _mockUnitOfWork.Setup(c => c.Contacts.GetAll()).ReturnsAsync(contacts);

            // Act
            var actionResult = await Controller.GetAll();
            var result = actionResult.Result as OkObjectResult;
            

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Get All
        [Fact]
        public async Task CreateContacts_ValidContacts_ReturnsCreatedContacs()
        {
            // Arrange
            var contacts = GetSampleContact();

            var contactDto = new CreateContactDto
            {
                Name = "newContact",
                Email = "newcontact@alkemy.com"
            };

            _mockUnitOfWork.Setup(s => s.Contacts.Create(It.IsAny<Contact>())).Verifiable("Created");
            _mockUnitOfWork.Setup(s => s.CompleteAsync()).Verifiable("Completed");

            _mailService.Setup(m => m.SendMail(It.IsAny<SendMailDto>())).Verifiable("Sent");

            // Act
            var actionResult = await Controller.Create(contactDto);


            // Assert
            var result = actionResult.Value;
            Assert.Equal(new int(), result);
          
            _mockUnitOfWork.Verify(v => v.Contacts.Create(It.Is<Contact>(
                a => a.Name == contactDto.Name && a.Email == contactDto.Email)), Times.Once());
            _mockUnitOfWork.Verify(v => v.CompleteAsync(), Times.Once);
        }
        #endregion



        private List<Contact> GetSampleContact()
        {
            var contacts = new List<Contact>
            {
                new ()
                {
                    Id = 1,
                    Name = "contactName1",
                    Phone = 1234567899,
                    Email = "contatcEmail1",
                    Message = "contatcMessage1"
                },
                 new ()
                {
                    Id = 2,
                    Name = "contactName2",
                    Phone = 234567899,
                    Email = "contatcEmail1",
                    Message = "contatcMessage1"
                },
                 new ()
                {
                    Id = 3,
                    Name = "contactName2",
                    Phone = 334567899,
                    Email = "contatcEmail1",
                    Message = "contatcMessage1"
                }
            };
            return contacts;
        }
    }
}
