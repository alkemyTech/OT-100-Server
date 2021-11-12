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
        
        //[DataType(DataType.Upload)]
        public string Image { get; set; }
        
    }
}