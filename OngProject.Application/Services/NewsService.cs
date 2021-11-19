using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.News;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Domain.Entities;

namespace OngProject.Application.Services
{
    public class NewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStore _fileStore;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper, IFileStore fileStore )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStore = fileStore;
        }
        // ==================== Get All ==================== //
        public async Task<IEnumerable<GetNewsDto>> GetNews()
        {
            var news = await _unitOfWork.News.GetAll();
            return news
                .AsQueryable()
                .ProjectTo<GetNewsDto>(_mapper.ConfigurationProvider);
        }

        // ==================== Get By Id ==================== //
         public async Task<GetNewsDetailsDto> GetNewsDetails(int id)
        {
            var news = await _unitOfWork.News.GetById(id);

            if (news is null)
                throw new NotFoundException(nameof(News), id);

            return _mapper.Map<GetNewsDetailsDto>(news);
        }
         
         // ==================== Get By Id Comments ==================== //
         public async Task<GetNewsDetailsCommentsDto> GetByIdComments(int id)
         {
             var news = await _unitOfWork.News.GetByIdComments(id);

             if (news is null)
                 throw new NotFoundException(nameof(News), id);

             return _mapper.Map<GetNewsDetailsCommentsDto>(news);
         }

        // ==================== Post News ==================== //
       public async Task<int> CreateNews(CreateNewsDto newsDto)
        {
            var url = await _fileStore.SaveFile(newsDto.Image);
            var news = _mapper.Map<News>(newsDto);
            news.Image = url;
          
            await _unitOfWork.News.Create(news);
            await _unitOfWork.CompleteAsync();

            return news.Id;
        }

        // ==================== Update News ==================== //
      public async Task<GetNewsDetailsDto> UpdateNews(int id, CreateNewsDto newsDto)
        {
            var news = await _unitOfWork.News.GetById(id);
            var url = await _fileStore.EditFile(newsDto.Image, news.Image);
            
            if (news is null)
                throw new NotFoundException(nameof(News), id);

            news.Id = id;
            
            await _unitOfWork.News.Update(_mapper.Map(newsDto, news));
            news.Image = url;
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetNewsDetailsDto>(news);

        }

        // ==================== Soft Delete News ==================== //
          public async Task SoftDeleteNews(int id)
        {
            var news = await _unitOfWork.News.GetById(id);
            
            if (news is null)
                throw new NotFoundException(nameof(News), id);

            await _unitOfWork.News.Delete(news);
            await _fileStore.DeleteFile(news.Image);
            await _unitOfWork.CompleteAsync();
        }

    }
}