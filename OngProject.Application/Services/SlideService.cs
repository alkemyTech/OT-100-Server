using System.Threading.Tasks;
using AutoMapper;
using OngProject.Application.DTOs.Slides;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Domain.Entities;

namespace OngProject.Application.Services
{
    public class SlideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SlideService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetSlideDetailsDto> GetSlideDetailsDto(int id)
        {
            var slide = await _unitOfWork.Slides.GetById(id);

            if (slide is null)
                throw new NotFoundException(nameof(Slide), id);

            return _mapper.Map<GetSlideDetailsDto>(slide);
        }

        public async Task UpdateSlide(int id, UpdateSlideDto slideDto)
        {
            var slide = await _unitOfWork.Slides.GetById(id);

            if (slide is null)
                throw new NotFoundException(nameof(Slide), id);

            slide.Id = id;
            slide = _mapper.Map(slideDto, slide);
            
            await _unitOfWork.Slides.Update(slide);
            await _unitOfWork.CompleteAsync();
        }
    }
}