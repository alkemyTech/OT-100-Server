using System.Threading.Tasks;
using OngProject.Application.DTOs;
using OngProject.Application.DTOs.Identity;

namespace OngProject.Application.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<string> Register(AuthRequestDto requestDto);

        Task<AuthResponseDto> Login(AuthRequestDto requestDto);

        Task<CurrentUserDto> Me();

    }
}