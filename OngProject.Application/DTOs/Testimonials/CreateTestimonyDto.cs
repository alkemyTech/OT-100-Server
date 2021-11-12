using System.ComponentModel.DataAnnotations;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Testimonials
{
    public class CreateTestimonyDto : IMapFrom<Testimony>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        //[DataType(DataType.Upload)]
        public string Image { get; set; }
        
        [Required]
        [StringLength(1200)]
        public string Content { get; set; }
    }
}