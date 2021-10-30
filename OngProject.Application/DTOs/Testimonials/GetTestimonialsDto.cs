using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Testimonials
{
    public class GetTestimonialsDto : IMapFrom<Testimony>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
    }
}
