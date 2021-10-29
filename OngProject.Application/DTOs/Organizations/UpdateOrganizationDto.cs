using Microsoft.AspNetCore.Http;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Organizations
{
    public class UpdateOrganizationDto : IMapFrom<Organization>
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Id { get; set; }
        [StringLength(60)]
        public string Name { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        [StringLength(1200)]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(500)]
        public string WelcomeText { get; set; }
        [StringLength(1500)]
        public string AboutUsText { get; set; }
    }
}
