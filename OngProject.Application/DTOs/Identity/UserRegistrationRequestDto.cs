using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Identity
{
    public class UserRegistrationRequestDto
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(60)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(16)]
        public string Password { get; set; }
    }
}