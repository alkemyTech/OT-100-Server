using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Categories
{
    public class UpdateCategoryDto : IMapFrom<Category>
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Id { get; set; }
        [StringLength(60)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [DataType(DataType.Upload)]
        public string Image { get; set; }
    }
}
