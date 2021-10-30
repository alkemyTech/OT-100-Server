using OngProject.Application.Mappings;
using OngProject.Domain.Entities;


namespace OngProject.Application.DTOs.Users
{
    public class GetUsersDto : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Photo { get; set; }
    }
}
