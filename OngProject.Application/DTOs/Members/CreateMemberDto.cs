using System.ComponentModel.DataAnnotations;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Members
{
    public class CreateMemberDto : IMapFrom<Member>
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
        public string Image { get; set; }
        
        [StringLength(1200)]
        public string Description { get; set; }
    }
}