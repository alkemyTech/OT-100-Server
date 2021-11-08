using System;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.UsersDetails
{
    public class GetUserDetailsDto : IMapFrom<UserDetails>
    {
        public Guid IdentityId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Photo { get; set; }
    }
}
