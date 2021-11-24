using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Organizations
{
    public class SlideOrgDto : IMapFrom<Slide>
    {
            public int Id { get; set; }
            public string Text { get; set; }
            public string ImageUrl { get; set; }
            public int Order { get; set; }
  
    }
}

