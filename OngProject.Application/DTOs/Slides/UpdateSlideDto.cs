using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Slides
{
    public class UpdateSlideDto : IMapFrom<Slide>
    {
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
    }
}