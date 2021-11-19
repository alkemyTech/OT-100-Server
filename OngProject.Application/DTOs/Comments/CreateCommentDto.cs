using System.ComponentModel.DataAnnotations;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Comments
{
    public class CreateCommentDto : IMapFrom<Comment>
    {
        [Required]
        public int UserDetailsId { get; set; }
        
        [Required]
        public int NewsId { get; set; }
        
        [Required]
        public string Body { get; set; }
    }
}