using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Contacts
{
    public class GetContactsDto : IMapFrom<Contact>
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public int Phone { set; get; }
        public string Email { set; get; }
    }
}