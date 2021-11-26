using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Application.DTOs.Slides;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.Services
{
    public class SlideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStore _fileStore;

        public SlideService(IUnitOfWork unitOfWork, IMapper mapper, IFileStore fileStore)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStore = fileStore;
        }

        public async Task<GetSlideDetailsDto> GetSlideDetailsDto(int id)
        {
            var slide = await _unitOfWork.Slides.GetById(id);

            if (slide is null)
                throw new NotFoundException(nameof(Slide), id);

            return _mapper.Map<GetSlideDetailsDto>(slide);
        }

        public async Task<int> CreateSlide(CreateSlideDto slideDto)
        {
            if (slideDto.Order == null)
            {
                var slideList = await _unitOfWork.Slides.GetAll();
                var lastSlide = slideList.Last();
                slideDto.Order = lastSlide.Order + 1;
            }

            var url = await _fileStore.SaveFile(slideDto.ImageUrl);
            var slide = _mapper.Map<Slide>(slideDto);
            slide.ImageUrl = url;

            await _unitOfWork.Slides.Create(slide);
            await _unitOfWork.CompleteAsync();

            return slide.Id;
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

        public async Task<List<GetSlidesDto>> GetSlides()
        {
            var slides = await _unitOfWork.Slides.GetAll();
            
            return slides
                .AsQueryable()
                .ProjectToList<GetSlidesDto>(_mapper.ConfigurationProvider);
        }

         public async Task SoftDeleteSlide(int id)
        {
            var slide = await _unitOfWork.Slides.GetById(id);
            
            if (slide is null)
                throw new NotFoundException(nameof(Slide), id);

            await _unitOfWork.Slides.Delete(slide);
            await _unitOfWork.CompleteAsync();
        }
    }
}