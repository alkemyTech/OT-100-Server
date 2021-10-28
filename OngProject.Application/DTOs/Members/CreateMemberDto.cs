using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Application.DTOs.Members
{
    public class CreateMemberDto
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        
        [DataType(DataType.Url)]
        [StringLength(120)]
        public string FacebookUrl { get; set; }
        
        [DataType(DataType.Url)]
        [StringLength(120)]
        public string InstagramUrl { get; set; }
        
        [DataType(DataType.Url)]
        [StringLength(120)]
        public string LinkedInUrl { get; set; }
        
        //[Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        
        [StringLength(1200)]
        public string Description { get; set; }
    }
}