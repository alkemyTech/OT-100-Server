using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.Dto.Organizations
{
    public class CreateOrganizationDto
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [StringLength(1200)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string WelcomeText { get; set; }

        [StringLength(1500)]
        public string AboutUsText { get; set; }
    }
}
