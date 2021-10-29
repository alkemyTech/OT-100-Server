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
            var categories = await _unitOfWork.Categories.GetById(id);
            GetCategoriesDto result = _mapper.Map<Category, GetCategoriesDto>(categories);
            return result;
        }

        public async Task<GetCategoriesDto> Add(CreateCategoryDto createCategoryDto)
        {
            Category category = _mapper.Map<CreateCategoryDto, Category>(createCategoryDto);
            Category categoryAdded = await _unitOfWork.Categories.Create(category);
            GetCategoriesDto getCategoriesDto = _mapper.Map<Category, GetCategoriesDto>(categoryAdded);
            return getCategoriesDto;
        }

        public async Task<GetCategoriesDto> Update(UpdateCategoryDto updateCategoryDto)
        {
            Category category = _mapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);
            Category categoryUpdated = await _unitOfWork.Categories.Update(category);
            GetCategoriesDto getCategoriesDto = _mapper.Map<Category, GetCategoriesDto>(categoryUpdated);
            return getCategoriesDto;
        }

        public async Task Delete(int id)
        {
            var categories = await _unitOfWork.Categories.GetById(id);
            await _unitOfWork.Categories.Delete(categories);
        }

    }
}
