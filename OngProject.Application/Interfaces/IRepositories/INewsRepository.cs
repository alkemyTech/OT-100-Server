using System.Threading.Tasks;
using OngProject.Domain.Entities;

namespace OngProject.Application.Interfaces.IRepositories
{
    public interface INewsRepository : IGenericRepository<News>
    {
        Task <News> GetByIdComments(int id);
    }
}
