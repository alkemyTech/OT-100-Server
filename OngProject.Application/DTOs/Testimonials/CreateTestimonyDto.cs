using Microsoft.AspNetCore.Http;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Testimonials
{
    public class CreateTestimonyDto : IMapFrom<Testimony>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [DataType(DataType.Upload)]
        public string Image { get; set; }

        [StringLength(1200)]
        public string Content { get; set; }
    }
}
