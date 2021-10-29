using System.Threading.Tasks;
using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Repositories.NewsRepository
{
    public interface INewsRepository : IGenericRepository<News>
    {
        
    }
}
