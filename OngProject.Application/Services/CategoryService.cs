using AutoMapper;
using OngProject.Application.DTOs.Categories;
using OngProject.Application.Exceptions;
using OngProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using OngProject.Application.Interfaces;
using OngProject.Application.Helpers.Wrappers;
using OngProject.Application.Mappings;

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
        

        public async Task<Pagination<GetCategoriesDto>> GetAll(CategoryQueryDto queryDto)
        {
            var categories = await _unitOfWork.Categories.GetAll();
            return categories
                .AsQueryable()
                .ProjectTo<GetCategoriesDto>(_mapper.ConfigurationProvider)
                .PaginatedResponse(queryDto.PageNumber, queryDto.PageSize);
        }

        public async Task<GetCategoryDetailsDto> GetById(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);

            if (category is null)// || category.DeletedAt != null)
                throw new NotFoundException(nameof(Category), id);

            return _mapper.Map<GetCategoryDetailsDto>(category);
        }

        public async Task<GetCategoriesDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);

            await _unitOfWork.Categories.Create(category);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetCategoriesDto>(category);
        }

        public async Task Update(int id, CreateCategoryDto updateCategory)
        {
            var category = await _unitOfWork.Categories.GetById(id);

            if (category is null)
                throw new NotFoundException(nameof(Category), id);

            await _unitOfWork.Categories.Update(_mapper.Map(updateCategory,category));
            await _unitOfWork.CompleteAsync();

        }

        public async Task Delete(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);

            if (category is null)
                throw new NotFoundException(nameof(Category), id);

            await _unitOfWork.Categories.Delete(category);
            await _unitOfWork.CompleteAsync();

        }

    }
}
