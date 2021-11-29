using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Contacts;
using OngProject.Application.DTOs.Mails;
using OngProject.Application.Interfaces;
using OngProject.Application.Interfaces.Mail;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;


namespace OngProject.Application.Services
{
    public class ContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<IEnumerable<GetContactsDto>> GetAll()
        {
            var contacts = await _unitOfWork.Contacts.GetAll();
            return contacts
                .AsQueryable()
                .ProjectToList<GetContactsDto>(_mapper.ConfigurationProvider);
        }


        public async Task<int> CreateContact(CreateContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);

            await _unitOfWork.Contacts.Create(contact);
            await _unitOfWork.CompleteAsync();

            var mail = new SendMailDto
            {
                Name = contactDto.Name,
                EmailTo = contactDto.Email,
                Subject = "Contacto Ong Somos Más",
                Text = "Gracias por ponerse en contacto con ONG Somos más"
            };
            await _mailService.SendMail(mail);

            return contact.Id;
        }
    }
}