using System.ComponentModel.DataAnnotations;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.UsersDetails
{
    public class UpdateUserDetailsDto : IMapFrom<UserDetails>
    {
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }
    }
}