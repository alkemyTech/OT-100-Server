using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.Collections.Generic;

namespace OngProject.Application.DTOs.Organizations
{
    public class GetOrganizationPublicDto : IMapFrom<Organization>
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedInUrl { get; set; }
        
        public List<SlideOrgDto> Slides { get; set; }

    }
}
