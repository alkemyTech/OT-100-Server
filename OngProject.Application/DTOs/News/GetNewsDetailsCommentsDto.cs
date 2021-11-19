using System.Collections.Generic;
using OngProject.Application.DTOs.Comments;
using OngProject.Application.Mappings;

namespace OngProject.Application.DTOs.News
{
    public class GetNewsDetailsCommentsDto : IMapFrom<Domain.Entities.News>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public List<GetCommentsDto> Comments { get; set; }
    }
}