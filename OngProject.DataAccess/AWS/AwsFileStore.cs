using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OngProject.Application.Interfaces;

namespace OngProject.DataAccess.AWS
{
    public class AwsFileStore : IFileStore
    {
        public AwsFileStore()
        {
        }
        
        public async Task<string> SaveFile(IFormFile image)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> EditFile(IFormFile image)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteFile(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}