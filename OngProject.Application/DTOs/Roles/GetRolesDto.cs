using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Roles
{
    public class GetRolesDto : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
 
        public string Description { get; set; }

    }
}
