using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Identity
{
    public class UserLoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(60)]
        public string Email { get; init; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(16)]
        public string Password { get; init; }
    }
}