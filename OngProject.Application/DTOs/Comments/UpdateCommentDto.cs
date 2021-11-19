using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Comments
{
    public class UpdateCommentDto : IMapFrom<Comment>
    {
        public string Body { get; set; }
    }
}
