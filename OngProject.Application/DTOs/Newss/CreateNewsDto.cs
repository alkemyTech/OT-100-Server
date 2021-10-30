using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Newss
{
    public class CreateNewsDto : IMapFrom<News>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        
        [StringLength(2000)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public string Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}