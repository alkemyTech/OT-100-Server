using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OngProject.Application.DTOs.Categories;
using OngProject.DataAccess.Interfaces;
using OngProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Application.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCategoriesDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAll();
            return categories
                .AsQueryable()
                .AsNoTracking()
                .ProjectTo<GetCategoriesDto>(_mapper.ConfigurationProvider);
        }

        public async Task<GetCategoriesDto> GetById(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);
            return _mapper.Map<GetCategoriesDto>(category);
        }

        public async Task<GetCategoriesDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);

            await _unitOfWork.Categories.Create(category);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetCategoriesDto>(category);
        }

        public async Task Update(UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);

            await _unitOfWork.Categories.Update(category);
            await _unitOfWork.CompleteAsync();

        }

        public async Task Delete(int id)
        {
            var categories = await _unitOfWork.Categories.GetById(id);
            await _unitOfWork.Categories.Delete(categories);
            await _unitOfWork.CompleteAsync();

        }

    }
}
