using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OngProject.Application.DTOs.Newss;
using OngProject.DataAccess.Interfaces;
using OngProject.Domain.Entities;

namespace OngProject.Application.Services
{
    public class NewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // ==================== Get All ==================== //
        public async Task<IEnumerable<GetNewsDto>> GetNews()
        {
            var news = await _unitOfWork.News.GetAll();
            return news
                .AsQueryable()
                .AsNoTracking()
                .ProjectTo<GetNewsDto>(_mapper.ConfigurationProvider);
        }

        // ==================== Get By Id ==================== //
         public async Task<GetNewsDetailsDto> GetNewsDetails(int id)
        {
            var news = await _unitOfWork.News.GetById(id);

            if (news is null)
                throw new Exception($"No se encontró la entidad {nameof(News)} de id ({id}).");

            return _mapper.Map<GetNewsDetailsDto>(news);
        }

        // ==================== Post News ==================== //
       public async Task<int> CreateNews(CreateNewsDto newsDto)
        {
            var news = _mapper.Map<News>(newsDto);

            await _unitOfWork.News.Create(news);
            await _unitOfWork.CompleteAsync();

            return news.Id;
        }

        // ==================== Update News ==================== //
      public async Task UpdateNews(int id, CreateNewsDto newsDto)
        {
            var news = await _unitOfWork.News.GetById(id);
            
            if (news is null)
                throw new Exception($"No se encontró la entidad {nameof(News)} de id ({id}).");

            news.Id = id;
            await _unitOfWork.News.Update(_mapper.Map(newsDto, news));
            await _unitOfWork.CompleteAsync();
        }

        // ==================== Soft Delete News ==================== //
          public async Task SoftDeleteNews(int id)
        {
            var news = await _unitOfWork.News.GetById(id);
            
            if (news is null)
                throw new Exception($"No se encontró la entidad {nameof(News)} de id ({id}).");

            await _unitOfWork.News.Delete(news);
            await _unitOfWork.CompleteAsync();
        }

    }
}