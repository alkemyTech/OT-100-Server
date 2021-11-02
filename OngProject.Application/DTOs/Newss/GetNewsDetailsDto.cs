using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Newss
{
    public class GetNewsDetailsDto : IMapFrom<News>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }


    }
}