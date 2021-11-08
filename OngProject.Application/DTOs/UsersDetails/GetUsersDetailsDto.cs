using OngProject.Application.Mappings;
using OngProject.Domain.Entities;


namespace OngProject.Application.DTOs.UsersDetails
{
    public class GetUsersDetailsDto : IMapFrom<UserDetails>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
