using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Repositories.CategoryRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task SoftDelete(Category category);

    }
}
