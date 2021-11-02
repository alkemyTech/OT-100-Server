using OngProject.Application.Interfaces.Identity;

namespace OngProject.DataAccess.Identity
{
    public class TokenParameters : ITokenParameters
    {
        public string Id { get; init; }
        public string UserName { get; init; }
        public string PasswordHash { get; init; }
    }
}