using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Users
{
    public class CreateUserDto : UpdateUserDto
    {
        [Required]
        [StringLength(240)]
        public string Photo { get; set; }
    }
}
