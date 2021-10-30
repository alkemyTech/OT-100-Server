using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Newss
{
    public class GetNewsDto : IMapFrom<News>
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
}