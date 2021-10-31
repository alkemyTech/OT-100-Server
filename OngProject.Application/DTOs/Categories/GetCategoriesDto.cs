using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Categories
{
    public class GetCategoriesDto : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
