using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Slides
{
    public class GetSlideDetailsDto : IMapFrom<Slide>
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }
    }
}