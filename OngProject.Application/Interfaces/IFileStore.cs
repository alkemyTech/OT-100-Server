using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OngProject.Application.Interfaces
{
    public interface IFileStore
    {
        Task<string> SaveFile(IFormFile image);

        Task<string> EditFile(IFormFile image);

        Task DeleteFile(string url);
    }
}