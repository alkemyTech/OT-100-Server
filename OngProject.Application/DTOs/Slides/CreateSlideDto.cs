using Microsoft.AspNetCore.Http;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Slides
{
    public class CreateSlideDto : IMapFrom<Slide>
    {
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ImageUrl { get; set; }
        [Required]
        [StringLength(8000)]
        public string Text { get; set; }
        public int? Order { get; set; }
        public int OrganizationId { get; set; }
    }
}
