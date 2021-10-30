using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Roles
{
    public class CreateRoleDto : IMapFrom<Role>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(1200)]
        public string Description { get; set; }
    }
}
