using OngProject.Application.Mappings;

namespace OngProject.Application.DTOs.News
{
    public class GetNewsDto : IMapFrom<Domain.Entities.News>
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
}