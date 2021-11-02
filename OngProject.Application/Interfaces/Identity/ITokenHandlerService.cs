namespace OngProject.Application.Interfaces.Identity
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters parameters);
    }
}