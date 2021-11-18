using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OngProject.Application.Mappings;

namespace OngProject.Application.DTOs.News
{
    public class CreateNewsDto : IMapFrom<Domain.Entities.News>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        
        [StringLength(2000)]
        public string Content { get; set; }
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}