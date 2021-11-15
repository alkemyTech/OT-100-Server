using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Slides
{
    public class GetSlidesDto : IMapFrom<Slide>
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
     
    }
}