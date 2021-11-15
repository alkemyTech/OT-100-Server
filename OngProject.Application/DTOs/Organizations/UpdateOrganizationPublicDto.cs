using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Organizations
{
    public class UpdateOrganizationPublicDto : IMapFrom<Organization>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [StringLength(1200)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [StringLength(200)]
        public string InstagramUrl { get; set; }

        [StringLength(200)]
        public string FacebookUrl { get; set; }

        [StringLength(200)]
        public string TwitterUrl { get; set; }
    }
}
