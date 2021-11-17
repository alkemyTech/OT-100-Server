using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Contacts;
using OngProject.Application.Interfaces;

namespace OngProject.Application.Services
{
    public class ContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<GetContactsDto>> GetAll()
        {
            var categories = await _unitOfWork.Contacts.GetAll();
            return categories
                .AsQueryable()
                .ProjectTo<GetContactsDto>(_mapper.ConfigurationProvider);
        }
    }
}