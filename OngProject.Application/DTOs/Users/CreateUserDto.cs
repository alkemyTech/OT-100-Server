using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Users
{
    public class CreateUserDto : IMapFrom<User>
    {
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }

        [Required]
        [StringLength(24)]
        public string Password { get; set; }

        [Required]
        [StringLength(240)]
        public string Photo { get; set; }
    }
}
