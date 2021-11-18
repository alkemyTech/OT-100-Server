using OngProject.Application.Mappings;

namespace OngProject.Application.DTOs.News
{
    public class GetNewsDetailsDto : IMapFrom<Domain.Entities.News>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}