using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Application.DTOs.Users
{
    public class GetUserDetailsDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Photo { get; set; }
        public int RoleId { get; set; }
    }
}
