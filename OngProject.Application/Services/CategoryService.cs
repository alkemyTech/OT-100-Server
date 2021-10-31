using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OngProject.Application.DTOs.Categories;
using OngProject.Application.Exceptions;
using OngProject.DataAccess.Interfaces;
using OngProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
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
               //.Where(m => m.DeletedAt == null) Hay que agregar la logica para las entidades eliminadas?
                .AsNoTracking()
                .ProjectTo<GetCategoriesDto>(_mapper.ConfigurationProvider);
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
