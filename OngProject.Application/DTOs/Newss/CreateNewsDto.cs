using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Application.DTOs.Newss
{
    public class CreateNewsDto
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        
        [StringLength(2000)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}