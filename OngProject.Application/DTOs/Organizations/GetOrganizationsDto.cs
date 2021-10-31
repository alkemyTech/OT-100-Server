using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Organizations
{
    public class GetOrganizationsDto : IMapFrom<Organization>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
