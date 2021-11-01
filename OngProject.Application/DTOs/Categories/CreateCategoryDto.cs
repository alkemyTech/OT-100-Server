using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Categories
{
    public class CreateCategoryDto : IMapFrom<Category>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
