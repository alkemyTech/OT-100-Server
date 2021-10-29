using OngProject.DataAccess.Repositories.GenericRepository;
using OngProject.Domain.Entities;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Repositories.CategoryRepository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task SoftDelete(Category category)
        {
            await Task.FromResult(DbContext.Categories.Remove(category));
        }
    }
}
