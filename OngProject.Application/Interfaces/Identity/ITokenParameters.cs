namespace OngProject.Application.Interfaces.Identity
{
    public interface ITokenParameters
    {
        string Id { get; init; }
        string UserName { get; init; }
        string PasswordHash { get; init; }
    }
}