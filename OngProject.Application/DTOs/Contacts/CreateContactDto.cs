using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Contacts
{
    public class CreateContactDto : IMapFrom<Contact>
    {
        [Required]
        [StringLength(60)]
        public string Name { set; get; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
    }
}
