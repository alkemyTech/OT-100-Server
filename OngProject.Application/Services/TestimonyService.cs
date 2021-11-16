using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Testimonials;
using OngProject.Application.Exceptions;
using OngProject.Application.Helpers.Wrappers;
using OngProject.Application.Interfaces;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.Services
{
    public class TestimonyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestimonyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Pagination<GetTestimonialsDto>> GetTestimonials(TestimonialsQueryDto queryDto)
        {
            var testimonials = await _unitOfWork.Testimonials.GetAll();

            return testimonials
                .AsQueryable()
                .ProjectTo<GetTestimonialsDto>(_mapper.ConfigurationProvider)
                .PaginatedResponse(queryDto.PageNumber, queryDto.PageSize);
        }

        public async Task<GetTestimonialsDto> GetTestimonyById(int id)
        {
            var testimony = await _unitOfWork.Testimonials.GetById(id);

            if (testimony is null)
                throw new NotFoundException(nameof(Testimony), id);

            return _mapper.Map<GetTestimonialsDto>(testimony);
        }

        public async Task<int> CreateTestimony (CreateTestimonyDto testimonyDto)
        {
            var testimony = _mapper.Map<Testimony>(testimonyDto);

            await _unitOfWork.Testimonials.Create(testimony);
            await _unitOfWork.CompleteAsync();

            return testimony.Id;
        }

        public async Task UpdateTestimony(int id, CreateTestimonyDto testimonyDto)
        {
            var testimony = await _unitOfWork.Testimonials.GetById(id);

            if (testimony is null)
                throw new NotFoundException(nameof(Testimony), id);

            testimony.Id = id;
            await _unitOfWork.Testimonials.Update(_mapper.Map(testimonyDto, testimony));
            await _unitOfWork.CompleteAsync();
        }

        public async Task SoftDeleteTestimony(int id)
        {
            var testimony = await _unitOfWork.Testimonials.GetById(id);

            if (testimony is null)
                throw new NotFoundException(nameof(Testimony), id);

            await _unitOfWork.Testimonials.Delete(testimony);
            await _unitOfWork.CompleteAsync();
        }
    }
}